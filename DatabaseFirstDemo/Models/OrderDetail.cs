using System;
using System.Collections.Generic;

namespace DatabaseFirstDemo.Models;

public partial class OrderDetail
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public bool Status { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
