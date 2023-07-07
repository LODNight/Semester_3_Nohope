using Providence.Models;

namespace Providence.Service;

public interface CategoryService
{
    public dynamic findAll();
    public bool Create(Category category);
    public bool Edit(Category category);
    public bool Delete(int id);
    public dynamic find(int id);
}
