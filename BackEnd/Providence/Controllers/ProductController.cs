using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Providence.Helper;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using Providence.Service.Implement.OutCRUD;
using Providence.Service.Interface;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Providence.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IServiceCRUD<Product> _serviceCRUD;
    private readonly IProductService _productService;
    private IWebHostEnvironment webHostEnvironment;
    private readonly IConfiguration configuration;
    public ProductController(IServiceCRUD<Product> serviceCRUD, IProductService productService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _serviceCRUD = serviceCRUD;
        _productService = productService;
        this.webHostEnvironment = webHostEnvironment;
        this.configuration = configuration;
    }


    // GET
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

    [Produces("application/json")]
    [HttpGet("product-inpq")]
    public IActionResult ProductINPQ()
    {
        try
        {
            return Ok(_productService.ReadINPQ());
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
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


    // PUT

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update")]
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
    
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Hide")]
    public IActionResult Hide(int id)
    {
        try
        {
            
            return Ok(_productService.HideProduct(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost("add-product")]
    public IActionResult UploadFiles(IFormFile[] files, IFormCollection formData)
    {
        try
        {
            var productFile = JsonConvert.DeserializeObject<Product>(formData["Product"]);
            return Ok(_productService.AddProduct(files,productFile));
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost("update-product")]
    public IActionResult UppdateProduct(int productId, IFormFile[] files, IFormCollection formData)
    {
        try
        {
            var productFile = JsonConvert.DeserializeObject<Product>(formData["Product"]);
            return Ok(_productService.UpdateProduct(productId,files,productFile));
        }
        catch
        {
            return BadRequest();
        }
    }

}
