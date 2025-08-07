using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses_Tracker;

public partial class Category
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsDefault { get; set; }
}
