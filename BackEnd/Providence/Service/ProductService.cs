using Providence.Models;

namespace Providence.Service;

public interface ProductService
{
    public dynamic findAll();
    public bool Create(Product product);
    public bool Edit(Product product);
    public bool Delete(int id);
    public dynamic find(int id);
}
