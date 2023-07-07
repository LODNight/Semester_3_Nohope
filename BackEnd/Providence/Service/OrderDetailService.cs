
using Providence.Models;

namespace Providence.Service;


public interface OrderDetailService
{
    // ====== Find / Search / Filter
    // Find
    public dynamic findOrderDetail(int id);

    public bool create(OrderDetail orderDetail);
    public bool update(OrderDetail orderDetail);

    // ====== Delete
    public bool Delete(int id);
}
