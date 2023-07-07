

using Providence.Models;
using System.Diagnostics;

namespace Providence.Service;

public class CouponsServiceImpl : CouponsService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    private CouponsService couponsService;

    public CouponsServiceImpl(DatabaseContext _db, IConfiguration configuration)
    {
        db = _db;
        this.configuration = configuration;
    }


    // ============================== 
    // Create
    // ============================== 
    public bool create(Coupon coupon)
    {
        try
        {
            db.Coupons.Add(coupon);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    // ============================== 
    // FInd
    // ============================== 

    // Find All
    public dynamic findAll()
    {
        return db.Coupons.Select(coupon => new
        {
            id = coupon.CouponId,
            couponname = coupon.CouponName,
            coupontypeid = coupon.CouponsTypeId,
            coupontypename = coupon.CouponsType.NameType,
            discount = coupon.Discount,
            discription = coupon.Description,
            createdAt = coupon.CreatedAt,
            updatedAt = coupon.UpdatedAt,
        }).ToList();
    }

    // Search By Name
    public dynamic searchByName(string name)
    {
        return db.Coupons.Where(p => p.CouponName.Contains(name)).Select(coupon => new
        {
            id = coupon.CouponId,
            couponname = coupon.CouponName,
            coupontypeid = coupon.CouponsTypeId,
            coupontypename = coupon.CouponsType.NameType,
            discount = coupon.Discount,
            discription = coupon.Description,
            createdAt = coupon.CreatedAt,
            updatedAt = coupon.UpdatedAt,
        }).ToList();
    }

    // Update
    public bool update(Coupon coupon)
    {
        try
        {
            db.Entry(coupon).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    
    // Delete 
    public bool Delete(int id)
    {
        try
        {
            db.Coupons.Remove(db.Coupons.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

   
}
