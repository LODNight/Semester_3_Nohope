using Providence.Models;
using Providence.Service.Interface;
using static Azure.Core.HttpHeader;

namespace Providence.Service.Implement;

public class BlogCRUD : IServiceCRUD<Blog>
{
    private readonly DatabaseContext _databaseContext;
    private IConfiguration configuration;

    public BlogCRUD(DatabaseContext databaseContext, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
    }

    public bool Create(Blog entity)
    {
        try
        {
            _databaseContext.Blogs.Add(entity);
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var blogEntity = _databaseContext.Blogs.FirstOrDefault(b => b.BlogId == id);
            if (blogEntity != null)
            {
                _databaseContext.Blogs.Remove(blogEntity);
                return _databaseContext.SaveChanges() > 0;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public dynamic Get(int id) => _databaseContext.Blogs.Where(p => p.BlogId == id).Select(p => new
    {
        blogId = p.BlogId,
        blogName = p.BlogName,
        blogImage = configuration["BaseUrl"] + "/images/" + p.BlogImage,
        shortDescription = p.ShortDescription,
        longDescription = p.LongDescription,
        hide = p.Hide,
        createdAt = p.CreatedAt,
        updatedAt = p.UpdatedAt,
    }).FirstOrDefault()!;

    public dynamic Read() => _databaseContext.Blogs.Select(p => new   
    {
        blogId = p.BlogId,
        blogName = p.BlogName,
        blogImage = configuration["BaseUrl"] + "/images/" + p.BlogImage,
        shortDescription = p.ShortDescription,
        longDescription = p.LongDescription,
        hide = p.Hide,
        createdAt = p.CreatedAt,
        updatedAt = p.UpdatedAt,
    }).ToList();

    public bool Update(Blog blog)
    {
        try
        {
            // Save Created At
            var existingBlog = _databaseContext.Blogs.FirstOrDefault(a => a.BlogId == blog.BlogId);
            if (existingBlog == null)
            {
                return false;
            }
            blog.CreatedAt = existingBlog.CreatedAt;

            // Cập nhật các thuộc tính khác
            _databaseContext.Entry(existingBlog).CurrentValues.SetValues(blog);
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
