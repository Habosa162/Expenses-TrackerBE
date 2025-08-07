using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses_Tracker;

public partial class TransactionType
{
    [Key]
    public int Id { get; set; }
    public string? Type { get; set; }
}
