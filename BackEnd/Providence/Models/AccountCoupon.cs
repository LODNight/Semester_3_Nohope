using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class AccountCoupon
{
    public int CouponId { get; set; }

    public int AccountId { get; set; }

    public bool? IsUsed { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Coupon Coupon { get; set; } = null!;
}
