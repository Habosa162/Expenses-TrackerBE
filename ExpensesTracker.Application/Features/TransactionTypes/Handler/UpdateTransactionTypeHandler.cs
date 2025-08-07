using AutoMapper;
using AutoMapper.Configuration.Annotations;
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
    public class UpdateTransactionTypeHandler : IRequestHandler<UpdateTransactionTypeCommand, TransactionTypeDto>
    {
        private readonly ITransactionTypesRepository _transactionTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UpdateTransactionTypeHandler(
            ITransactionTypesRepository transactionTypesRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionTypeRepository = transactionTypesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TransactionTypeDto> Handle(UpdateTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionType = await _transactionTypeRepository.Get(request.Id);
                if (transactionType == null)
                {
                    var ex = new Exception();
                    throw ex;
                }
                var newTansactionType = new TransactionType()
                {
                    Type = request.Type
                };
                var updatedTransactionType =  await _transactionTypeRepository.Update(newTansactionType);
                var transactionTypeDto = _mapper.Map<TransactionTypeDto>(updatedTransactionType);
                return transactionTypeDto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: UpdateTransactionTypeHandler Method: Handle()");
                throw;
            }
        }
    }
}
