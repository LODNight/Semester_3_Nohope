using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Providence.Helper;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Providence.Controllers;
[Route("api/account")]
public class AccountController : Controller
{
    private AccountService accountService;
    private IConfiguration configuration;
    //private readonly IJwtService _jwtService;


    public AccountController(AccountService accountService, IConfiguration configuration)
    {
        this.accountService = accountService;
        this.configuration = configuration;
    }

    [HttpGet("protected-resource")]
    [Authorize(Roles = "admin")]
    public IActionResult GetProtectedResource()
    {
        return Ok("This is a protected resource for admin only.");
    }


    // ===============================
    // ============== GET GET
    // ===============================

    // Find All
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {
        try
        {
            return Ok(accountService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
    
    // Find
    [Produces("application/json")]
    [HttpGet("find/{id}")]
    public IActionResult Find(int id)
    {
        try
        {
            return Ok(accountService.find(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // Find By Email
    [Produces("application/json")]
    [HttpGet("findbyEmail/{email}")]
    public IActionResult findbyEmail(string email)
    {
        try
        {
            return Ok(accountService.findbyEmail(email));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    } 

    // Find By Name
    [Produces("application/json")]
    [HttpGet("findbyName/{keyword}")]
    public IActionResult findbyName(string keyword)
    {
        try
        {
            return Ok(accountService.findbyName(keyword));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    } 

    // Find By Status
    [Produces("application/json")]
    [HttpGet("findbyStatus/{status}")]
    public IActionResult findbyStatus(bool status)
    {
        try
        {
            return Ok(accountService.findbyStatus(status));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // ===============================
    // ============== POST
    // ===============================

    // Login
    [Produces("application/json")]
    [HttpPost("login")]
    public IActionResult Login([FromBody] Account account)
    {

        if (!accountService.CheckMail(account.Email))
        {
            return BadRequest("Email not found");
        }

        try
        {
            string token = CreateToken(account);
            return Ok(token);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }


    // Create Token  
    private string CreateToken(Account account)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Email)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds    
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    // Create New Account
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("register")]
    public IActionResult Register([FromBody] Account account)
    {
        try
        {
            if (accountService.CheckMail(account.Email))
            {
                return BadRequest("Email have Exist");
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Status = false;
            account.SecurityCode = RandomHelper.RandomString(4);
            // Mail


            if (accountService.register(account))
            {
                //Send mail
                var content = "Security Code: " + account.SecurityCode ;
                content += "<br><hr><br>";
                content += "<a href='http://localhost:5271/api/account/verify;email="+ account.Email+ "&securityCode="+ account.SecurityCode +"'>Click here to Verify Email</a>";
                var mailHelper = new MailHelper(configuration);
                mailHelper.Send(configuration["Gmail:Username"], account.Email, "Verify", content);

                return Ok(account.Email);

            }
            else
            {
                return BadRequest("Wrong");
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }

        
    }
    
    // Create New Account
    [Produces("application/json")]
    [HttpPut("verify")]
    public IActionResult VerifyCode(Verify verify)
    {
        return Ok(accountService.Active(verify));
    }


    // ===============================
    // ============== PUT
    // ===============================

    // Update Information Account
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updateinformation")]
    public IActionResult UpdateInformation([FromBody] Account account)
    {
        try
        {
            account.CreatedAt = account.CreatedAt;
            account.UpdatedAt = DateTime.Now;

            return Ok(new
            {

                status = accountService.updateInformation(account)
            });

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }


    // Change Pass
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("changePassword")]
    public IActionResult ChangePass([FromBody] Account account)
    {
        try
        {
            account.UpdatedAt = DateTime.Now;
            if (string.IsNullOrEmpty(account.Password))
            {
                account.Password = accountService.findByIdNoTracking(account.AccountId).Password;
            }
            else
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }

            return Ok(new
            {

                status = accountService.changePass(account)
            });

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // ===============================
    // ============== DELETE
    // ===============================

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(new
            {
                status = accountService.Delete(id)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
