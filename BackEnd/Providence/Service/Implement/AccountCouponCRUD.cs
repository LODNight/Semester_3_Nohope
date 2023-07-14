using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class AccountCouponCRUD : IServiceCRUD<AccountCoupon>
    {
        private readonly DatabaseContext _databaseContext;

        public AccountCouponCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(AccountCoupon entity)
        {
            try
            {
                _databaseContext.AccountCoupons.Add(entity);
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
                var accountCouponEntity = _databaseContext.AccountCoupons.FirstOrDefault(ac => ac.CouponId == id);
                if (accountCouponEntity != null)
                {
                    _databaseContext.AccountCoupons.Remove(accountCouponEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.AccountCoupons.FirstOrDefault(ac => ac.CouponId == id);

        public dynamic Read() => _databaseContext.AccountCoupons.ToList();

        public bool Update(AccountCoupon entity)
        {
            try
            {
                _databaseContext.AccountCoupons.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
