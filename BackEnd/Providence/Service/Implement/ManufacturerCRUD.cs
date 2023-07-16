using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class ManufacturerCRUD : IServiceCRUD<Manufacturer>
    {
        private readonly DatabaseContext _databaseContext;

        public ManufacturerCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Manufacturer entity)
        {
            try
            {
                _databaseContext.Manufacturers.Add(entity);
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
                var manufacturerEntity = _databaseContext.Manufacturers.FirstOrDefault(m => m.MftId == id);
                if (manufacturerEntity != null)
                {
                    _databaseContext.Manufacturers.Remove(manufacturerEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Manufacturers.Where(m => m.MftId == id).Select(p => new
        {
            mftId = p.MftId,
            mftName = p.MftName,
            mftAddress = p.Address,
            mftDescription = p.MftDescription
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.Manufacturers.Select(p => new
        {
            mftId = p.MftId,
            mftName = p.MftName,
            mftAddress = p.Address,
            mftDescription = p.MftDescription
        }).ToList();

        public bool Update(Manufacturer entity)
        {
            try
            {
                _databaseContext.Manufacturers.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
