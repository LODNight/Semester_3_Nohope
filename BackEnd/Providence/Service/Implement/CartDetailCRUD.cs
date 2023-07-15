using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class CartDetailCRUD : IServiceCRUD<CartDetail>
    {
        private readonly DatabaseContext _databaseContext;

        public CartDetailCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(CartDetail entity)
        {
            try
            {
                _databaseContext.CartDetails.Add(entity);
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
                var cartDetailEntity = _databaseContext.CartDetails.FirstOrDefault(cd => cd.CartDetailId == id);
                if (cartDetailEntity != null)
                {
                    _databaseContext.CartDetails.Remove(cartDetailEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.CartDetails.Where(cd => cd.CartDetailId == id).Select(p => new
        {
            cartDetailId = p.CartDetailId,
            cartId = p.CartId,
            productId = p.ProductId,
            productName = p.Product.ProductName,
            quantity = p.Quantity,
            price = p.Price,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.CartDetails.Select(p => new
        {
            cartDetailId = p.CartDetailId,
            cartId = p.CartId,
            productId = p.ProductId,
            productName = p.Product.ProductName,
            quantity = p.Quantity,
            price = p.Price,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).ToList();

        public bool Update(CartDetail entity)
        {
            try
            {
                _databaseContext.CartDetails.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
