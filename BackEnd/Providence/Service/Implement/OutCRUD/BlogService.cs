using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");

            // Kiểm tra xem có tệp tin ảnh được cung cấp hay không
            if (file == null)
            {
                return false;
            }

            // Tạo tên tệp tin mới
            var fileName = FileHelper.generateFileName(file.FileName);

            // Tạo đường dẫn lưu trữ tệp tin
            var filePath = Path.Combine(uploadFolderPath, fileName);

            // Lưu tệp tin mới
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Cập nhật tên ảnh cho blog
            blog.BlogImage = fileName;

            // Thêm blog vào cơ sở dữ liệu
            _databaseContext.Blogs.Add(blog);
            _databaseContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ nếu cần thiết
            return false;
        }
    }


    public bool UpdateBlog(int blogId, IFormFile file, Blog blog)
    {
        try
        {
            var existingBlog = _databaseContext.Blogs.Find(blogId);
            if (existingBlog == null)
            {
                return false;
            }

            var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");

            // Kiểm tra xem có tệp tin ảnh mới được cung cấp hay không
            if (file != null)
            {
                // Xóa ảnh cũ (nếu có)
                if (!string.IsNullOrEmpty(existingBlog.BlogImage))
                {
                    var existingImagePath = Path.Combine(uploadFolderPath, existingBlog.BlogImage);
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }
                }

                // Tạo tên tệp tin mới
                var fileName = FileHelper.generateFileName(file.FileName);

                // Tạo đường dẫn lưu trữ tệp tin
                var filePath = Path.Combine(uploadFolderPath, fileName);

                // Lưu tệp tin mới
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Cập nhật tên ảnh mới cho blog
                existingBlog.BlogImage = fileName;
            }

            // Cập nhật thông tin cần thiết của blog
            existingBlog.BlogName = blog.BlogName;
            existingBlog.ShortDescription = blog.ShortDescription;
            existingBlog.LongDescription = blog.LongDescription;
            existingBlog.Hide = blog.Hide;
            existingBlog.UpdatedAt = DateTime.Now;

            // Lưu các thay đổi vào cơ sở dữ liệu
            

            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ nếu cần thiết
            return false;
        }
    }
}
