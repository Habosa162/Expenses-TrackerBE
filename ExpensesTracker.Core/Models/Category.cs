using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure;

public partial class Category
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public bool? IsDefault { get; set; }
}
