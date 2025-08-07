using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Transactions.Queries;
using ExpensesTracker.Core.Abstraction.Repositories;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Transactions.Handler
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, GetAllTransactionsDTO>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
      
        
        public GetAllTransactionsHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<GetAllTransactionsDTO> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _transactionRepository.GetQueryableTransactions()
                    .Where(t => t.UserId == request.UserId)
                    .Include(t=>t.Type)
                    .Include(t=>t.Category)
                    .ToListAsync(); 
                
                



                var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
                var totalIncome = transactions.Where(t => t.Type.Type == "Income").Sum(t => t.Amount ?? 0);
                var totalExpenses = transactions.Where(t => t.Type.Type == "Expense").Sum(t => t.Amount ?? 0); 

                return new GetAllTransactionsDTO {
                    Transactions = transactionDtos,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpenses
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: GetAllTransactionsHandler Method: Handle()");
                throw;
            }
        }

    }
}
