using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class OrderDetailCRUD : IServiceCRUD<OrderDetail>
    {
        private readonly DatabaseContext _databaseContext;

        public OrderDetailCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(OrderDetail entity)
        {
            try
            {
                _databaseContext.OrderDetails.Add(entity);
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
                var orderDetailEntity = _databaseContext.OrderDetails.FirstOrDefault(od => od.OrderDetailId == id);
                if (orderDetailEntity != null)
                {
                    _databaseContext.OrderDetails.Remove(orderDetailEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.OrderDetails.Where(od => od.OrderDetailId == id).Select(p => new
        {
            orderDetailId = p.OrderDetailId,
            orderId = p.OrderId,
            accountEmail = p.Order.Account.Email,
            product = p.Product,
            quantity = p.Quantity,
            price = p.Price,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.OrderDetails.Select(p => new
        {
            orderDetailId = p.OrderDetailId,
            orderId = p.OrderId,
            accountEmail = p.Order.Account.Email,
            product = p.Product,
            quantity = p.Quantity,
            price = p.Price,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).ToList();

        public bool Update(OrderDetail entity)
        {
            try
            {
                _databaseContext.OrderDetails.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
