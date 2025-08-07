using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Commands
{
    public class UpdateTransactionCommand : IRequest<TransactionDto>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
    }
}
