using AutoMapper;
using ExpensesTracker.Application.Features.TransactionTypes.Commands;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Repositories;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Handler
{
    public class DeleteTransactionTypeHandler : IRequestHandler<DeleteTransactionTypeCommand, bool>
    {
        private readonly ITransactionTypesRepository _transactionTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public DeleteTransactionTypeHandler(
            ITransactionTypesRepository transactionTypesRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionTypeRepository = transactionTypesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
               return await _transactionTypeRepository.Delete(request.Id);    
            }
            catch (Exception ex) {
                _logger.Error(ex, "Class: DeleteTransactionTypeHandler Method: Handle()");
                throw;
            }
        }
    }
}
