using AutoMapper;
using Expenses_Tracker;
using ExpensesTracker.Application.DTOs;
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
    public class CreateTransactionTypeHandler : IRequestHandler<CreateTransactionTypeCommand, TransactionTypeDto>
    {
        private readonly ITransactionTypesRepository _transactionTypesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CreateTransactionTypeHandler(
            ITransactionTypesRepository transactionTypesRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionTypesRepository = transactionTypesRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<TransactionTypeDto> Handle(CreateTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newTransactionType = new TransactionType() { 
                    Type = request.Type
                };
                var addedTransactionType = await _transactionTypesRepository.Add(newTransactionType);
                if (addedTransactionType == null) {
                    throw new Exception(); 
                }
                var transactionDto = _mapper.Map<TransactionTypeDto>(addedTransactionType);
                return transactionDto; 
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: CreateTransactionTypeHandler Method: Handle()");
                throw;
            }
        }
    }
}
