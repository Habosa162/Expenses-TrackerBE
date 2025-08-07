using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Queries
{
    public class GetAllTransactionsQuery: IRequest<GetAllTransactionsDTO>
    {
        public string UserId { get; set; }
    }
}
