using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Diagnostics;
using static Azure.Core.HttpHeader;
namespace Providence.Controllers;
[Route("api/[controller]")]
public class WardController : Controller
{
    private readonly IServiceCRUD<Ward> _serviceCRUD;
    private readonly IWardService wardService;
    private IConfiguration configuration;

    public WardController(IServiceCRUD<Ward> serviceCRUD, IWardService wardService, IConfiguration configuration)
    {
        _serviceCRUD = serviceCRUD;
        this.wardService = wardService;
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


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("find/{id}")]
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
    [HttpGet("find-ward-by-district/{code}")]
    public IActionResult FindWardByDistrict(string code)
    {
        try
        {
            return Ok(wardService.FindWardByDistrict(code));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Ward ward)

    {
        try
        {
            return Ok(_serviceCRUD.Create(ward));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
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
    public IActionResult Update([FromBody] Ward ward)
    {
        try
        {
            return Ok(_serviceCRUD.Update(ward));
        }
        catch
        {
            return BadRequest();
        }
    }

}
