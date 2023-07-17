using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Diagnostics;
namespace Providence.Controllers;
[Route("api/[controller]")]
public class CartDetailController : Controller
{
    private readonly IServiceCRUD<CartDetail> _serviceCRUD;
    private readonly ICartDetailService cartDetailService;

    public CartDetailController(IServiceCRUD<CartDetail> serviceCRUD, ICartDetailService cartDetailService)
    {
        _serviceCRUD = serviceCRUD;
        this.cartDetailService = cartDetailService;
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
    [HttpPost("create")]
    public IActionResult Create([FromBody] CartDetail cartDetail)

    {
        try
        {

            return Ok(_serviceCRUD.Create(cartDetail));
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
    public IActionResult Update([FromBody] CartDetail cartDetail)
    {
        try
        {
            return Ok(_serviceCRUD.Update(cartDetail));
        }
        catch
        {
            return BadRequest();
        }
    }

}