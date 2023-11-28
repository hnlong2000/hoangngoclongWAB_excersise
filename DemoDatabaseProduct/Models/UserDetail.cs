using System;
using System.Collections.Generic;

namespace DemoDatabaseProduct.Models;

public partial class UserDetail
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
