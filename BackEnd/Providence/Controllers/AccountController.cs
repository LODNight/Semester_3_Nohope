using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;
using System.Globalization;


namespace Providence.Controllers;
[Route("api/account")]
public class AccountController : Controller
{
    private AccountService accountService;

    public AccountController(AccountService accountService)
    {
        this.accountService = accountService;
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
        try
        {
            return Ok(new
            {
                status = accountService.login(account.Email, account.Password)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // Create New Account
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("register")]
    public IActionResult Register([FromBody] Account account)
    {
        try
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Status = true;
            account.SecurityCode = RandomHelper.RandomString(4);

            return Ok(new
            {
                status = accountService.register(account)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
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



}
