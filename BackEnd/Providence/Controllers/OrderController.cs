using Microsoft.AspNetCore.Mvc;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Interface;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IServiceCRUD<Order> _serviceCRUD;
    private readonly IOrderService orderService;

    public OrderController(IServiceCRUD<Order> serviceCRUD, IOrderService orderService)
    {
        _serviceCRUD = serviceCRUD;
        this.orderService = orderService;
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
    public IActionResult Create([FromBody] Order order)

    {
        try
        {
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;


            return Ok(_serviceCRUD.Create(order));
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
    public IActionResult Update([FromBody] Order order)
    {
        try
        {
            order.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(order));
        }
        catch
        {
            return BadRequest();
        }
    }

}
