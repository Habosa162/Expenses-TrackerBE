using AutoMapper;
using ExpensesTracker.Application.Features.Transactions.Commands;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Repositories;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Handler
{
    public class DeleteTransactionHandler :  IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger _logger;
        public DeleteTransactionHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Information("Class: DeleteTransactionHandler Method: Handle() - Deleting transaction with Id: {Id}", request.Id);
                return await _transactionRepository.Delete(request.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: DeleteTransactionHandler Method: Handle()");
                throw; 
            }
        }
    }
}
