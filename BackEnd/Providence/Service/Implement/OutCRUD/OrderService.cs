using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class OrderService : IOrderService
{
    private readonly DatabaseContext _databaseContext;

    public OrderService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

 

}
