using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Diagnostics;
namespace Providence.Controllers;
[Route("api/[controller]")]
public class CartController : Controller
{
    private readonly IServiceCRUD<Cart> _serviceCRUD;
    private readonly ICartService cartService;
    private IConfiguration configuration;

    public CartController(IServiceCRUD<Cart> serviceCRUD, IConfiguration configuration, ICartService cartService)
    {
        _serviceCRUD = serviceCRUD;
        this.configuration = configuration;
        this.cartService = cartService;
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
    public IActionResult Create([FromBody] Cart cart)

    {
        try
        {
            cart.CreatedAt = DateTime.Now;
            cart.UpdatedAt = DateTime.Now;


            return Ok(_serviceCRUD.Create(cart));
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
    public IActionResult Update([FromBody] Cart cart)
    {
        try
        {
            cart.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(cart));
        }
        catch
        {
            return BadRequest();
        }
    }

}