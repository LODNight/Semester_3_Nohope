

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

    public BlogServiceImpl(DatabaseContext _db, IConfiguration configuration)
    {
        db = _db;
        this.configuration = configuration;
    }


    // ============================== 
    // Create
    // ============================== 
    public bool create(Blog blog)
    {
        try
        {
            db.Blogs.Add(blog);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    // Delete 
    public bool Delete(int id)
    {
        try
        {
            db.Blogs.Remove(db.Blogs.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
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
            name = blog.BlogName,
            image = blog.BlogImage,
            shortdescription = blog.ShortDescription,
            longdescription = blog.LongDescription,
            hide = blog.Hide,
            createdAt = blog.CreatedAt,
            updatedAt = blog.UpdatedAt,
        }).ToList();
    }


    // Update
    public bool update(Blog blog)
    {
        try
        {
            db.Entry(blog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
