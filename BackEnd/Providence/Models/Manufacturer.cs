﻿using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class Manufacturer
{
    public int MftId { get; set; }

    public string? MftName { get; set; }

    public int? AddressId { get; set; }

    public string? MftDescription { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
