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
    public class GetAllTransactionTypeHandler : IRequestHandler<GetAllTransactionTypeQuery, IEnumerable<TransactionTypeDto>>
    {
        private readonly ITransactionTypesRepository _transactionTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllTransactionTypeHandler(
            ITransactionTypesRepository transactionTypesRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _transactionTypeRepository = transactionTypesRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<TransactionTypeDto>> Handle(GetAllTransactionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionTypes = await _transactionTypeRepository.GetAll();
                var transactionTypesDtos = _mapper.Map<IEnumerable<TransactionTypeDto>>(transactionTypes);

                return transactionTypesDtos; 
            
            }
            catch (Exception ex) {
                _logger.Error(ex, "Class: GetAllTransactionTypeHandler Method: Handle()");
                throw;
            }
        }
    }
}
