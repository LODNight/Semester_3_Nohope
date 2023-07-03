using Microsoft.AspNetCore.Mvc;
using Providence.Service;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/blog")]
public class BlogController : Controller
{
    private BlogService blogService;

    public BlogController(BlogService blogService)
    {
        this.blogService = blogService;
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
            return Ok(blogService.findAll());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
