using Microsoft.AspNetCore.Mvc;
using Providence.Helpers;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/order")]
public class OrderController : Controller
{
    private OrderService orderService;
    private OrderDetailService orderDetailService;

    public OrderController(OrderService orderService, OrderDetailService orderDetailService)
    {
        this.orderService = orderService;
        this.orderDetailService = orderDetailService;
    }
    // ===============================
    // ============== GET GET
    // ===============================

    // Find All
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {
        try
        {
            return Ok(orderService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // =============================
    // ========== Order Detail

    // Find Order Detail
    [Produces("application/json")]
    [HttpGet("findOrderDetail/{id}")]
    public IActionResult FindOrderDetail(int id)
    {
        try
        {
            return Ok(orderDetailService.findOrderDetail(id));
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
    // Create New Order
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Order order)
    {
        try
        {
            return Ok(new
            {
                status = orderService.create(order)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // =============================
    // ========== Order Detail
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("createDetail")]
    public IActionResult CreateOrderDetail([FromBody] OrderDetail orderDetail)
    {
        try
        {
            return Ok(new
            {
                status = orderDetailService.create(orderDetail)
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

    // Update Information Order
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult Update([FromBody] Order order)
    {
        try
        {
            order.UpdatedAt = DateTime.Now;

            return Ok(new
            {
                status = orderService.update(order)
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
                status = orderService.Delete(id)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
