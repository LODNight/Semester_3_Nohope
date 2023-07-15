using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class OrderDetailService : IOrderDetailService
{
    private readonly DatabaseContext _databaseContext;

    public OrderDetailService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

 

}
