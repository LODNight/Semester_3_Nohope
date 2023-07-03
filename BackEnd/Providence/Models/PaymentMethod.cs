﻿using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class PaymentMethod
{
    public int PaymentId { get; set; }

    public string? PaymentName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
