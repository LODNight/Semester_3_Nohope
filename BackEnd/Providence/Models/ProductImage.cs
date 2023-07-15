using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class ProductImage
{
    public int ImageId { get; set; }
    
    public string? ImageUrl { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
