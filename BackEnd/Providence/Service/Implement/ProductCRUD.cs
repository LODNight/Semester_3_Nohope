using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class ProductCRUD : IServiceCRUD<Product>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Product entity)
        {
            try
            {
                _databaseContext.Products.Add(entity);
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
                var productEntity = _databaseContext.Products.FirstOrDefault(p => p.ProductId == id);
                if (productEntity != null)
                {
                    _databaseContext.Products.Remove(productEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Products.FirstOrDefault(p => p.ProductId == id);

        public dynamic Read() => _databaseContext.Products.ToList();

        public bool Update(Product entity)
        {
            try
            {
                _databaseContext.Products.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
