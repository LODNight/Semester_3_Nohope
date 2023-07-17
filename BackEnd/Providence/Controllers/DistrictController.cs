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
public class DistrictController : Controller
{
    private readonly IServiceCRUD<District> _serviceCRUD;
    private readonly IDistrictService districtService;
    private IConfiguration configuration;

    public DistrictController(IServiceCRUD<District> serviceCRUD, IDistrictService districtService)
    {
        _serviceCRUD = serviceCRUD;
        this.districtService = districtService;
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
    [HttpGet("find-district-by-province")]
    public IActionResult findDistrictByProvince(string provinceId)
    {
        try
        {
            return Ok(districtService.FindDistrictByProvince(provinceId));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] District district)

    {
        try
        {
            return Ok(_serviceCRUD.Create(district));
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
    public IActionResult Update([FromBody] District district)
    {
        try
        {
            return Ok(_serviceCRUD.Update(district));
        }
        catch
        {
            return BadRequest();
        }
    }

}
