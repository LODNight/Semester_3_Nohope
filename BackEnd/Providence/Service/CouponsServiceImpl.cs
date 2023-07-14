﻿

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
<<<<<<< HEAD
            coupouName = coupon.CouponName,
            couponTypeid = coupon.CouponsTypeId,
            couponTypeName = coupon.CouponsType.NameType,
=======
            couponname = coupon.CouponName,
            coupontypeid = coupon.CouponTypeId,
            coupontypename = coupon.CouponType.NameType,
>>>>>>> 1cfeb420416562a3e8825dbb3f49819c01314827
            discount = coupon.Discount,
            discription = coupon.Description,
            expiredDay = coupon.ExpiredDay,
            createdAt = coupon.CreatedAt,
<<<<<<< HEAD
            expiredAt = coupon.ExpiredAt,
=======
>>>>>>> 1cfeb420416562a3e8825dbb3f49819c01314827
        }).ToList();
    }

    // Search By Name
    public dynamic searchByName(string name)
    {
        return db.Coupons.Where(p => p.CouponName.Contains(name)).Select(coupon => new
        {
            id = coupon.CouponId,
            couponname = coupon.CouponName,
            coupontypeid = coupon.CouponTypeId,
            coupontypename = coupon.CouponType.NameType,
            discount = coupon.Discount,
            discription = coupon.Description,
            expiredDay = coupon.ExpiredDay,
            createdAt = coupon.CreatedAt,
<<<<<<< HEAD
            expiredAt = coupon.ExpiredAt,
=======
>>>>>>> 1cfeb420416562a3e8825dbb3f49819c01314827
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
