using ExpensesTracker.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Commands
{
    public class UpdateTransactionTypeCommand : IRequest<TransactionTypeDto>
    {
        public int Id { get; set; }
        public string? Type { get; set; }
    }
}
