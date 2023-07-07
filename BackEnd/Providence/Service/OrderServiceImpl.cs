


using Providence.Models;
using System.Diagnostics;

namespace Providence.Service;

public class OrderServiceImpl : OrderService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    private OrderService orderService;

    public OrderServiceImpl(DatabaseContext _db, IConfiguration configuration)
    {
        db = _db;
        this.configuration = configuration;
    }


    // ============================== 
    // Create
    // ============================== 
    public bool create(Order order)
    {
        try
        {
            db.Orders.Add(order);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    // ============================== 
    // FInd
    // ============================== 

    // Find All
    public dynamic findAll()
    {
        return db.Orders.Select(order => new
        {
            id = order.OrderId,
            accountId = order.AccountId,
            accountName = order.Account.Firstname + ' ' + order.Account.Lastname,
            totalPrice = order.TotalPrice,
            orderStatusId = order.OrderStatusId,
            orderStatus = order.OrderStatus.StatusName,
            couponId = order.CouponId,
            couponName = order.Coupon.CouponName,
            createdAt = order.CreatedAt,
            updatedAt = order.UpdatedAt,
        }).ToList();
    }

    // Update
    public bool update(Order order)
    {
        try
        {
            db.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    
    // Delete 
    public bool Delete(int id)
    {
        try
        {
            db.Orders.Remove(db.Orders.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    
}
