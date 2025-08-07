using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Queries
{
    public class GetOneTransactionQuery : IRequest<TransactionDto>
    {
        public int Id { get; set; }
    }
}
