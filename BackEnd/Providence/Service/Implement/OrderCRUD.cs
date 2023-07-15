using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class OrderCRUD : IServiceCRUD<Order>
    {
        private readonly DatabaseContext _databaseContext;

        public OrderCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Order entity)
        {
            try
            {
                _databaseContext.Orders.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderEntity = _databaseContext.Orders.FirstOrDefault(o => o.OrderId == id);
                if (orderEntity != null)
                {
                    _databaseContext.Orders.Remove(orderEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Orders.Where(o => o.OrderId == id).Select(p => new
        {
            orderId = p.OrderId,
            accountName = p.Account.Firstname + ' ' + p.Account.Lastname,
            totalPrice = p.TotalPrice,
            orderStatus = p.OrderStatus.StatusName,
            payment = p.Payment.PaymentName,
            coupon = p.Coupon.CouponName,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.Orders.Select(p => new 
        {
            orderId = p.OrderId,
            accountName = p.Account.Firstname + ' ' + p.Account.Lastname,
            totalPrice = p.TotalPrice,
            orderStatus = p.OrderStatus.StatusName,
            payment = p.Payment.PaymentName,
            coupon = p.Coupon.CouponName,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).ToList();

        public bool Update(Order entity)
        {
            try
            {
                _databaseContext.Orders.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
