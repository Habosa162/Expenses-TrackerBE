using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Commands
{
    public class DeleteTransactionTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
