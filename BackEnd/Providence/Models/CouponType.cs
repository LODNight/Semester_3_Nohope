using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class CouponType
{
    public int CouponTypeId { get; set; }

    public string? TypeName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
