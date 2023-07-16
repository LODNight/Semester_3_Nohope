using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Providence.Helper;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace Providence.Controllers;
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IServiceCRUD<Account> _serviceCRUD;
    private readonly IAccountService accountService;
    private IConfiguration configuration;

    public AccountController(IServiceCRUD<Account> serviceCRUD, IAccountService accountService, IConfiguration configuration)
    {
        _serviceCRUD = serviceCRUD;
        this.accountService = accountService;
        this.configuration = configuration;
    }

    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_serviceCRUD.Read());
        }
        catch
        {
            return BadRequest();
        }
    }
    
    // Show Active When false
    [Produces("application/json")]
    [HttpGet("show-account-active")]
    public IActionResult ShowAccountActive()
    {
        try
        {
            return Ok(accountService.ShowAccountActive());
        }
        catch
        {
            return BadRequest();
        }
    }



    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("find")]
    public IActionResult Get(int id)
    {
        try
        {
            return Ok(_serviceCRUD.Get(id));
        }
        catch
        {
            return BadRequest();
        }
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("register")]
    public IActionResult Create([FromBody] Account account)

    {
        try
        {
            if (accountService.CheckMail(account?.Email))
            {
                return BadRequest("Email already exists");
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Status = false;
            account.SecurityCode = RandomHelper.RandomString(4);

            if (_serviceCRUD.Create(account))
            {
                var content = "Security Code: " + account.SecurityCode;
                content += "<br><hr><br>";
                var verifyUrl = $"http://localhost:5271/api/account/verify?email={account.Email}&securityCode={account.SecurityCode}";
                content += $"<a href='{verifyUrl}'>Click here to Verify Email</a>";

                var mailHelper = new MailHelper(configuration);
                mailHelper.Send(configuration["Gmail:Username"], account.Email, "Verify", content);

                return Ok(account.Email);
            }
            else
            {
                return BadRequest("Failed to register account");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(_serviceCRUD.Delete(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult Update([FromBody] Account account)
    {
        try
        {
            return Ok(_serviceCRUD.Update(account));
        }
        catch
        {
            return BadRequest();
        }

    }
    
    [Consumes("application/json")]
    [HttpPut("changePass")]
    public IActionResult ChangePass([FromBody] ChangePass changePass)
    {
        try
        {
            return Ok(accountService.ChangePass(changePass));
        }
        catch
        {
            return BadRequest();
        }
    }
}
