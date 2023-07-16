using Microsoft.AspNetCore.Hosting;
using Providence.Helper;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Diagnostics;

namespace Providence.Service.Implement.OutCRUD;

public class BlogService : IBlogService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public BlogService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    public dynamic Hide(int blogId)
    {
        var blog = _databaseContext.Blogs.Find(blogId);
        if (blog != null)
        {
            blog.Hide = !blog.Hide;
            return _databaseContext.SaveChanges() > 0;
        }
        else
        {
            return false;
        }
    }

    public dynamic ShowBlogsUnhide() => _databaseContext.Blogs.Where(p => p.Hide == false).ToList();

    public bool AddBlog(IFormFile file, Blog blog)
    {
        try
        {
            _databaseContext.Blogs.Add(blog);
            _databaseContext.SaveChanges();

            var urls = new List<string>();
            Debug.WriteLine("File Info");
            if (file != null && file.Length > 0)
            {
                var fileName = FileHelper.generateFileName(file.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                urls.Add(configuration["BaseUrl"] + fileName);

            }
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool UpdateBlog(int blogId, IFormFile file, Blog blog)
    {
        try
        {
            var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "upload");

            // Tiến hành tải lên hình ảnh
            var fileName = FileHelper.generateFileName(file.FileName);
            var path = Path.Combine(uploadFolderPath, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
