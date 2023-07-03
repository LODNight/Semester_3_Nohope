

using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Providence.Models;
using System.Diagnostics;
using System.Globalization;

namespace Providence.Service;

public class BlogServiceImpl : BlogService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    private AccountService accountService;

    public BlogServiceImpl(DatabaseContext _db, IConfiguration configuration)
    {
        db = _db;
        this.configuration = configuration;
    }

    // ============================== 
    // FInd
    // ============================== 

    // Find All
    public dynamic findAll()
    {
        return db.Blogs.Select(blog => new
        {
            id = blog.BlogId,
            shortdescription = blog.ShortDescription,
            longdescription = blog.LongDescription,
            hide = blog.Hide,
            createdAt = blog.CreatedAt,
            updatedAt = blog.UpdatedAt,
        }).ToList();
    }

    
}
