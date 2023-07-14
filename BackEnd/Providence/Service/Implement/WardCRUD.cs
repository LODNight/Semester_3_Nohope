using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class WardCRUD : IServiceCRUD<Ward>
    {
        private readonly DatabaseContext _databaseContext;

        public WardCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Ward entity)
        {
            try
            {
                _databaseContext.Wards.Add(entity);
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
                var wardEntity = _databaseContext.Wards.FirstOrDefault(w => w.Code == id.ToString());
                if (wardEntity != null)
                {
                    _databaseContext.Wards.Remove(wardEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Wards.FirstOrDefault(w => w.Code == id.ToString());

        public dynamic Read() => _databaseContext.Wards.ToList();

        public bool Update(Ward entity)
        {
            try
            {
                _databaseContext.Wards.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
