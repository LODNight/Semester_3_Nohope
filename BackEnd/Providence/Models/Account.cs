using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public int? RoleId { get; set; }

    public bool? Status { get; set; }

    public string? SecurityCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
