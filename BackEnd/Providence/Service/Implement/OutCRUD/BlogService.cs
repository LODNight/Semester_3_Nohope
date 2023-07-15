using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class BlogService : IBlogService
{
    private readonly DatabaseContext _databaseContext;

    public BlogService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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


}
