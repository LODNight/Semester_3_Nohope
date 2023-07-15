using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Providence.Models;
using Providence.Service.Implement;
using Providence.Service.Implement.OutCRUD;
using Providence.Service.Interface;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IServiceCRUD<Product> _serviceCRUD;
    //private readonly ProductService _productService;
    public ProductController(IServiceCRUD<Product> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
        //_productService = productService;
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

    //[Produces("application/json")]
    //[HttpGet("ProductINPQ")]
    //public IActionResult ProductINPQ()
    //{
    //    try
    //    {
    //        return Ok(_productService.ReadINPQ());
    //    }
    //    catch
    //    {
    //        return BadRequest();
    //    }
    //}

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] Product product)

    {
        try
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            return Ok(_serviceCRUD.Create(product));
        }
        catch
        {
            return BadRequest();
        }
    }

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

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] Product product)
    {
        try
        {
            product.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(product));
        }
        catch
        {
            return BadRequest();
        }
    }
}
