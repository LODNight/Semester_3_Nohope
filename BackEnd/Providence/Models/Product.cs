using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public string? Detail { get; set; }

    public string? ExpireDate { get; set; }

    public int? ManufacturerId { get; set; }

    public bool? Hide { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Category? Category { get; set; } 

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
