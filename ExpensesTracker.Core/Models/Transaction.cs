using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure;

[Keyless]
public partial class Transaction
{
    public int? ID { get; set; }

    [Column(TypeName = "decimal(10, 5)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [StringLength(100)]
    public string? Note { get; set; }

    [StringLength(450)]
    public string UserID { get; set; } = null!;

    public int CategoryID { get; set; }

    public int TypeID { get; set; }

    [ForeignKey("CategoryID")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("TypeID")]
    public virtual TransactionType Type { get; set; } = null!;
}
