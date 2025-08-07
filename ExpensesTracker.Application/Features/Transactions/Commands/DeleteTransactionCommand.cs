using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Commands
{
    public class DeleteTransactionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
