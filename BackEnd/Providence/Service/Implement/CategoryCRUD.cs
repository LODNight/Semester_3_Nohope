using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class CategoryCRUD : IServiceCRUD<Category>
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Category entity)
        {
            try
            {
                _databaseContext.Categories.Add(entity);
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
                var categoryEntity = _databaseContext.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (categoryEntity != null)
                {
                    _databaseContext.Categories.Remove(categoryEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Categories.FirstOrDefault(c => c.CategoryId == id);

        public dynamic Read() => _databaseContext.Categories.ToList();

        public bool Update(Category entity)
        {
            try
            {
                _databaseContext.Categories.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
