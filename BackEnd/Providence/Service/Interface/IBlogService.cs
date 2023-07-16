using Providence.Models;

namespace Providence.Service.Interface;

public interface IBlogService
{
    public dynamic Hide (int blogId);
    public dynamic ShowBlogsUnhide();
    public bool AddBlog(IFormFile[] file, Blog blog);
    public bool UpdateBlog(int blogId, IFormFile[] file, Blog blog);
}
