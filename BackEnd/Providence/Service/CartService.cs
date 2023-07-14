using Providence.Models;

namespace Providence.Service;

public interface CartService
{
    public dynamic findAll();
    public bool Create(Cart cart);
    public bool Edit(Cart cart);
    public bool Delete(int id);
    public dynamic find(int id);
}
