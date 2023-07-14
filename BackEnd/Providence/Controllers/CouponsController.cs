using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;
using System.Globalization;


namespace Providence.Controllers;
[Route("api/coupons")]
public class CouponsController : Controller
{
    private CouponsService couponsService;

    public CouponsController(CouponsService couponsService)
    {
        this.couponsService = couponsService;
    }

    // ===============================
    // ============== GET
    // ===============================

    // Find All
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {
        try
        {
            return Ok(couponsService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // Find All
    [Produces("application/json")]
    [HttpGet("searchByName/{name}")]
    public IActionResult SearchByName(string name)
    {
        try
        {
            return Ok(couponsService.searchByName(name));
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

    // Create New Coupons
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Coupon coupon)
    {
        try
        {
            return Ok(new
            {
                status = couponsService.create(coupon)
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

    // Update Information Coupons
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult UpdateInformation([FromBody] Coupon coupon)
    {
        try
        {

            return Ok(new
            {

                status = couponsService.update(coupon)
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
                status = couponsService.Delete(id)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

}
