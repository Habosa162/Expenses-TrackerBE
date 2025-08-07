using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.DTOs
{
    public class TransactionDto
    {
        public int? Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public string? UserId { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
