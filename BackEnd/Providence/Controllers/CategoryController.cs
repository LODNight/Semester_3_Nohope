using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service;
using System.Diagnostics;
using static Azure.Core.HttpHeader;

namespace Providence.Controllers;
[Route("api/category")]
public class CategoryController : Controller
{
    private CategoryService categoryService;
    private IWebHostEnvironment webHostEnvironment;

    public CategoryController(CategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        this.categoryService = categoryService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        try
        {
            return Ok(categoryService.findAll());
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
            return Ok(categoryService.find(id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] Category category)
    {
        try
        {
            return Ok(new
            {
                status = categoryService.Create(category)
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
    public IActionResult Edit([FromBody] Category category)
    {
        try
        {
            category.UpdatedAt = DateTime.Now;
            return Ok(new
            {
                status = categoryService.Edit(category)
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
                status = categoryService.Delete(id)
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
