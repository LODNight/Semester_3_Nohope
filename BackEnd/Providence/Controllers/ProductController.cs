using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/product")]
public class ProductController : Controller
{
    private ProductService productService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;

    public ProductController(ProductService productService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        this.productService = productService;
        this.webHostEnvironment = webHostEnvironment;
        this.configuration = configuration;
    }

    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        try
        {
            return Ok(productService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("findDetailById/{id}")]
    public IActionResult FindDetailById(int id)
    {
        try
        {
            return Ok(productService.findDetailById(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("searchByKeyword/{keyword}")]
    public IActionResult SearchByKeyword(string keyword)
    {
        try
        {
            return Ok(productService.searchByKeyword(keyword));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("edit/{id}/{newHide}")]
    public IActionResult hideProduct(int id, int newHide) //nhớ tải Microsoft.Data.SqlClient về mới chạy đoạn dưới
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Product SET hide = @newHide WHERE product_id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newHide", newHide);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return NotFound("Product not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("find/{id}")]
    public IActionResult Find(int id)
    {
        try
        {
            return Ok(productService.find(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Product product)
    {
        try
        {
            return Ok(productService.Create(product));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPut("edit")]
    public IActionResult Edit([FromBody] Product product)
    {
        try
        {
            return Ok(productService.Edit(product));
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
            return Ok(productService.Delete(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
