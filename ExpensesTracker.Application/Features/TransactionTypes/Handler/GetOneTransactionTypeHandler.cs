using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.TransactionTypes.Queries;
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
    public class GetOneTransactionTypeHandler : IRequestHandler<GetOneTransactionTypeQuery, TransactionTypeDto>
    {
        private readonly ITransactionTypesRepository _transactionTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetOneTransactionTypeHandler(
            ITransactionTypesRepository transactionTypesRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionTypeRepository = transactionTypesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TransactionTypeDto> Handle(GetOneTransactionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionType = await _transactionTypeRepository.Get(request.Id);
                if (transactionType == null)
                {
                    var ex = new Exception();
                    throw ex; 
                }
                var transactionTypeDto = _mapper.Map<TransactionTypeDto>(transactionType);
                return transactionTypeDto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: GetAllTransactionTypeHandler Method: Handle()");
                throw;
            }
        }
    }
}
