using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class CartCRUD : IServiceCRUD<Cart>
    {
        private readonly DatabaseContext _databaseContext;

        public CartCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Cart entity)
        {
            try
            {
                _databaseContext.Carts.Add(entity);
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
                var cartEntity = _databaseContext.Carts.FirstOrDefault(c => c.CartId == id);
                if (cartEntity != null)
                {
                    _databaseContext.Carts.Remove(cartEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Carts.FirstOrDefault(c => c.CartId == id);

        public dynamic Read() => _databaseContext.Carts.ToList();

        public bool Update(Cart entity)
        {
            try
            {
                _databaseContext.Carts.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
