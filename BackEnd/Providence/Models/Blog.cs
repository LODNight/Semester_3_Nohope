using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? BlogName { get; set; }

    public string? BlogImage { get; set; }

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public bool? Hide { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
