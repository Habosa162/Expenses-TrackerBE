using AutoMapper;
using ExpensesTracker.Application.DTOs;
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
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UpdateTransactionHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var DbTransaction = await _transactionRepository.Get(request.Id);
                if (DbTransaction == null)
                {
                    throw new KeyNotFoundException($"Transaction with Id {request.Id} does not exist.");
                }
                _logger.Information("Class: UpdateTransactionHandler Method: Handle() - Updating transaction with Id: {Id}", request.Id);
                var updatedTransaction = _mapper.Map(request, DbTransaction);
                var savedTransaction = await _transactionRepository.Update(updatedTransaction);
                if (savedTransaction == null)
                {
                    throw new InvalidOperationException("Failed to update transaction.");
                }
                var transactionDto = _mapper.Map<TransactionDto>(savedTransaction);
                return transactionDto;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: UpdateTransactionHandler Method: Handle()");
                throw;
            }
        }
    }
}
