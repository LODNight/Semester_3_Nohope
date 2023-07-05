
using Providence.Models;

namespace Providence.Service;


public interface BlogService
{

    // ====== Find / Search / Filter
    // Find
    public dynamic findAll();
    public bool create(Blog blog);
    public bool update(Blog blog);
    
}
