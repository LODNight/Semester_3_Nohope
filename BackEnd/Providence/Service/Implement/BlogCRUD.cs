using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement;

public class BlogCRUD : IServiceCRUD<Blog>
{
    private readonly DatabaseContext _databaseContext;

    public BlogCRUD(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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

    public dynamic Get(int id) => _databaseContext.Blogs.FirstOrDefault(b => b.BlogId == id);

    public dynamic Read() => _databaseContext.Blogs.ToList();

    public bool Update(Blog entity)
    {
        try
        {
            _databaseContext.Blogs.Update(entity);
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
