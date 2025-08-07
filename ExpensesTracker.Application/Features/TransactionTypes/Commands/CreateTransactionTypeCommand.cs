using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Commands
{
    public class CreateTransactionTypeCommand : IRequest<TransactionTypeDto>
    {
        public string? Type { get; set; }
    }
}
