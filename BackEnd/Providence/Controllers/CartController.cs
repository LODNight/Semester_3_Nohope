using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/cart")]
public class CartController : Controller
{
    private CartService cartService;
    private IWebHostEnvironment webHostEnvironment;

    public CartController(CartService cartService, IWebHostEnvironment webHostEnvironment)
    {
        this.cartService = cartService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        try
        {
            return Ok(cartService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("find/{id}")]
    public IActionResult Find(int id)
    {
        try
        {
            return Ok(cartService.find(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Cart cart)
    {
        try
        {
            return Ok(new
            {
                status = cartService.Create(cart)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPut("edit")]
    public IActionResult Edit([FromBody] Cart cart)
    {
        try
        {
            return Ok(new
            {
                status = cartService.Edit(cart)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(new
            {
                status = cartService.Delete(id)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
