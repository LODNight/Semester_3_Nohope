using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class CouponTypeCRUD : IServiceCRUD<CouponType>
    {
        private readonly DatabaseContext _databaseContext;

        public CouponTypeCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(CouponType entity)
        {
            try
            {
                _databaseContext.CouponTypes.Add(entity);
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
                var couponTypeEntity = _databaseContext.CouponTypes.FirstOrDefault(ct => ct.CouponTypeId == id);
                if (couponTypeEntity != null)
                {
                    _databaseContext.CouponTypes.Remove(couponTypeEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.CouponTypes.FirstOrDefault(ct => ct.CouponTypeId == id);

        public dynamic Read() => _databaseContext.CouponTypes.ToList();

        public bool Update(CouponType entity)
        {
            try
            {
                _databaseContext.CouponTypes.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
