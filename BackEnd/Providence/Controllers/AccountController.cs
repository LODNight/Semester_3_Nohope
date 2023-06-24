using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace Providence.Controllers;
[Route("api/account")]
public class AccountController : Controller
{
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {

        string output = "Account done";
        try
        {
            return Ok(output);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
