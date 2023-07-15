using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Diagnostics;
using System.Globalization;
using static Azure.Core.HttpHeader;


namespace Providence.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CouponController : Controller
{
    private readonly IServiceCRUD<Coupon> _serviceCRUD;
    public CouponController(IServiceCRUD<Coupon> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
        
    }

    // GET
    [Produces("application/json")]
    [HttpGet("Read")]
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

    [Produces("application/json")]
    [HttpGet("Get")]
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
    [HttpPost("Create")]
    public IActionResult Create([FromBody] Coupon coupon)

    {
        try
        {
            coupon.CreatedAt = DateTime.Now;

            return Ok(_serviceCRUD.Create(coupon));
        }
        catch
        {
            return BadRequest();
        }
    }

    // Delete

    [Produces("application/json")]
    [HttpDelete("Delete")]
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


    // PUT

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] Coupon coupon)
    {
        try
        {
            return Ok(_serviceCRUD.Update(coupon));
        }
        catch
        {
            return BadRequest();
        }
    }

}