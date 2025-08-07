using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Queries
{
    public class GetAllTransactionTypeQuery : IRequest<IEnumerable<TransactionTypeDto>>  
    {
    }
}
