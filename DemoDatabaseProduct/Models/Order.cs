using System;
using System.Collections.Generic;

namespace DemoDatabaseProduct.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime DateBuy { get; set; }

    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
}
