using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Transactions.Queries;
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
    internal class GetOneTransactionsHandler : IRequestHandler<GetOneTransactionQuery, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetOneTransactionsHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _logger = logger;
            _transactionRepository = transactionRepository;

        }
        public async Task<TransactionDto> Handle(GetOneTransactionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = await _transactionRepository.Get(request.Id);
                if (transaction == null)
                {
                    throw new KeyNotFoundException($"Transaction with Id {request.Id} does not exist.");
                }
                var transactionDto = _mapper.Map<TransactionDto>(transaction);
                return transactionDto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: GetOneTransactionsHandler Method: Handle()");
                throw;
            }
        }
    }
}
