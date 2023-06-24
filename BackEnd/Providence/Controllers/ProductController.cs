using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace Providence.Controllers;
[Route("api/product")]
public class ProductController : Controller
{
    //private OrderService orderService;

    //public OrderController(OrderService orderService)
    //{
    //    this.orderService = orderService;
    //}

    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {
        string input = "Done Yet";
        try
        {
            return Ok(input);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
