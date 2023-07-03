using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class CouponsType
{
    public int CouponsTypeId { get; set; }

    public string? NameType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
