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
        private IConfiguration configuration;

        public ProductCRUD(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            this.configuration = configuration;
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

        public dynamic Get(int id) => _databaseContext.Products.Where(p => p.ProductId == id).Select(product => new
        {
            id = product.ProductId,
            name = product.ProductName,
            price = product.Price,
            category = product.Category.CategoryName,
            quantity = product.Quantity,
            detail = product.Detail,
            expiredAt = product.ExpireDate,
            description = product.Description,
            photo = configuration["BaseUrl"] + "/images/" + product.ProductImages.First().ImageUrl,
            status = product.Hide,
            mftId = product.Manufacturer.MftId,
            mftName = product.Manufacturer.MftName,
            mftAddress = product.Manufacturer.Address,
            mftDescription = product.Manufacturer.MftDescription,
            created = product.CreatedAt,
            updated = product.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.Products.Select(product => new
        {
            id = product.ProductId,
            name = product.ProductName,
            price = product.Price,
            category = product.Category.CategoryName,
            quantity = product.Quantity,
            detail = product.Detail,
            expiredAt = product.ExpireDate,
            description = product.Description,
            photo = configuration["BaseUrl"] + "/images/" + product.ProductImages.First().ImageUrl,
            status = product.Hide,
            mftId = product.Manufacturer.MftId,
            mftName = product.Manufacturer.MftName,
            mftAddress = product.Manufacturer.Address,
            mftDescription = product.Manufacturer.MftDescription,
            created = product.CreatedAt,
            updated = product.UpdatedAt,
        }).ToList();

        public bool Update(Product product)
        {
            try
            {
                // Tìm kiếm Product theo id
                var existingProduct = _databaseContext.Products.FirstOrDefault(a => a.ProductId == product.ProductId);
                if (existingProduct == null)
                {
                    return false; // Không tìm thấy Product
                }
                product.CreatedAt = existingProduct.CreatedAt;

                // Cập nhật các thuộc tính khác
                _databaseContext.Entry(existingProduct).CurrentValues.SetValues(product);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
