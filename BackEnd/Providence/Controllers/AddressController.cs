using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service;
using Providence.Service.Implement;
using System.Diagnostics;

namespace Providence.Controllers;
[Route("api/[controller]")]
public class AddressController : Controller
{
    private readonly IServiceCRUD<Address> _serviceCRUD;

    public AddressController(IServiceCRUD<Address> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
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
    [HttpGet("find/{id}")]
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
    public IActionResult Create([FromBody] Address address)

    {
        try
        {
            return Ok(_serviceCRUD.Create(address));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
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
    public IActionResult Update([FromBody] Address address)
    {
        try
        {
            return Ok(_serviceCRUD.Update(address));
        }
        catch
        {
            return BadRequest();
        }
    }

}
