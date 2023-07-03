using System;
using System.Collections.Generic;

namespace Providence.Models;

public partial class InvoiceStatus
{
    public int InvoiceStatusId { get; set; }

    public string? StatusName { get; set; }

    public string? StatusDescription { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
