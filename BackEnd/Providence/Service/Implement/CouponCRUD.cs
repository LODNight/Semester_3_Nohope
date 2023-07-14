using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class CouponCRUD : IServiceCRUD<Coupon>
    {
        private readonly DatabaseContext _databaseContext;

        public CouponCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Coupon entity)
        {
            try
            {
                _databaseContext.Coupons.Add(entity);
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
                var couponEntity = _databaseContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (couponEntity != null)
                {
                    _databaseContext.Coupons.Remove(couponEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Coupons.FirstOrDefault(c => c.CouponId == id);

        public dynamic Read() => _databaseContext.Coupons.ToList();

        public bool Update(Coupon entity)
        {
            try
            {
                _databaseContext.Coupons.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
