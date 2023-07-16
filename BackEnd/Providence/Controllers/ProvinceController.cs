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
public class ProvinceController : Controller
{
    private readonly IServiceCRUD<Province> _serviceCRUD;
    private IConfiguration configuration;

    public ProvinceController(IServiceCRUD<Province> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
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
    [HttpPost("create")]
    public IActionResult Create([FromBody] Province province)

    {
        try
        {
            return Ok(_serviceCRUD.Create(province));
        }
        catch
        {
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
    public IActionResult Update([FromBody] Province province)
    {
        try
        {
            return Ok(_serviceCRUD.Update(province));
        }
        catch
        {
            return BadRequest();
        }
    }

}
