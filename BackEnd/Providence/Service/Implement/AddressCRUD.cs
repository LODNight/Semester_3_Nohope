using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class AddressCRUD : IServiceCRUD<Address>
    {
        private readonly DatabaseContext _databaseContext;

        public AddressCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Address entity)
        {
            try
            {
                _databaseContext.Addresses.Add(entity);
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
                var addressEntity = _databaseContext.Addresses.FirstOrDefault(a => a.AddressId == id);
                if (addressEntity != null)
                {
                    _databaseContext.Addresses.Remove(addressEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Addresses.FirstOrDefault(a => a.AddressId == id);

        public dynamic Read() => _databaseContext.Addresses.ToList();

        public bool Update(Address entity)
        {
            try
            {
                _databaseContext.Addresses.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
