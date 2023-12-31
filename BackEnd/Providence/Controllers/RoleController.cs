﻿using Microsoft.AspNetCore.Mvc;
using Providence.Models;
using Providence.Service.Implement;
using System.Data;

namespace Providence.Controllers;
[Route("api/[controller]")]
public class RoleController : Controller
{
    private readonly IServiceCRUD<Role> _serviceCRUD;
    private IConfiguration configuration;

    public RoleController(IServiceCRUD<Role> serviceCRUD)
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
    public IActionResult Create([FromBody] Role role)

    {
        try
        {
            return Ok(_serviceCRUD.Create(role));
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
    public IActionResult Update([FromBody] Role role)
    {
        try
        {
            return Ok(_serviceCRUD.Update(role));
        }
        catch
        {
            return BadRequest();
        }
    }

}
