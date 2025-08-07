using ExpensesTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses_Tracker;

public partial class Transaction
{
    [Key]
    public int Id { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }

    public string? Note { get; set; }

    public string UserId { get; set; } = null!;

    public int CategoryId { get; set; }

    public int TypeId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual TransactionType Type { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
