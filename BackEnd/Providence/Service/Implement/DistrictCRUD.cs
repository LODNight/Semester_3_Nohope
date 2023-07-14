using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class DistrictCRUD : IServiceCRUD<District>
    {
        private readonly DatabaseContext _databaseContext;

        public DistrictCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(District entity)
        {
            try
            {
                _databaseContext.Districts.Add(entity);
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
                var districtEntity = _databaseContext.Districts.FirstOrDefault(d => d.Code == id);
                if (districtEntity != null)
                {
                    _databaseContext.Districts.Remove(districtEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Districts.FirstOrDefault(d => d.Code == id);

        public dynamic Read() => _databaseContext.Districts.ToList();

        public bool Update(District entity)
        {
            try
            {
                _databaseContext.Districts.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
