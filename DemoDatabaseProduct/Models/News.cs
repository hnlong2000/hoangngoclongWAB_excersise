using System;
using System.Collections.Generic;

namespace DemoDatabaseProduct.Models;

public partial class News
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Descripition { get; set; }

    public string? SubjectContent { get; set; }

    public DateTime? DateUpdate { get; set; }

    public bool Status { get; set; }

    public string Avatar { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual NewsCategory Category { get; set; } = null!;

    public virtual User? User { get; set; }
}
