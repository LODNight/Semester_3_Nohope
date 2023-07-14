using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public int? CouponTypeId { get; set; }

    public string? CouponName { get; set; }

    public int? Discount { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AccountCoupon> AccountCoupons { get; set; } = new List<AccountCoupon>();

    public virtual CouponType? CouponType { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
