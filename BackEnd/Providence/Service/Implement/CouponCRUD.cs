using Providence.Models;
using static Azure.Core.HttpHeader;

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

        public dynamic Get(int id) => _databaseContext.Coupons.Where(c => c.CouponId == id).Select(p => new
        {
            couponId = p.CouponId,
            couponName = p.CouponName,
            discount = p.Discount,
            description = p.Description,
            expiredAt = p.ExpiredAt,
            createdAt = p.CreatedAt,
            couponTypeId = p.CouponTypeId,
            couponTypeName = p.CouponType.TypeName,
        }).FirstOrDefault();

        public dynamic Read() => _databaseContext.Coupons.Select(p => new
        {
            couponId = p.CouponId,
            couponName = p.CouponName,
            discount = p.Discount,
            description = p.Description,
            expiredAt = p.ExpiredAt,
            createdAt = p.CreatedAt,
            couponTypeId = p.CouponTypeId,
            couponTypeName = p.CouponType.TypeName,
        }).ToList();

        public bool Update(Coupon coupon)
        {
            try
            {
                // Save Created At
                var existingCoupon = _databaseContext.Coupons.FirstOrDefault(a => a.CouponId == coupon.CouponId);
                if (existingCoupon == null)
                {
                    return false; 
                }
              coupon.CreatedAt = existingCoupon.CreatedAt;

                // Cập nhật các thuộc tính khác
                _databaseContext.Entry(existingCoupon).CurrentValues.SetValues(coupon);
                return _databaseContext.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
