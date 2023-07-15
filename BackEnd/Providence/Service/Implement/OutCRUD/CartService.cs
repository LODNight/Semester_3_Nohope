using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class CartService : ICartService
{
    private readonly DatabaseContext _databaseContext;

    public CartService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }


}
