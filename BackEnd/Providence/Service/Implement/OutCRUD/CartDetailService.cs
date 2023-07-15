using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class CartDetailService : ICartDetailService
{
    private readonly DatabaseContext _databaseContext;

    public CartDetailService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }


}
