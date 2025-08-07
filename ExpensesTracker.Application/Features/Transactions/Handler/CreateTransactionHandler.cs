using AutoMapper;
using Expenses_Tracker;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Transactions.Commands;
using ExpensesTracker.Core.Abstraction.Repositories;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ExpensesTracker.Application.Features.Transactions.Handler
{
    internal class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CreateTransactionHandler(
            ITransactionRepository transactionRepository
            , IMapper mapper
            , ILogger logger
            )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _logger = logger;   
        }
        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newTransaction = new Transaction()
                {
                    Amount = request.Amount,
                    Note = request.Note,
                    Date = request.Date,
                    CategoryId = request.CategoryId,
                    TypeId = request.TypeId,
                    UserId = request.UserId,
                };
                //var newTransaction = _mapper.Map<Expenses_Tracker.Transaction>(request);   
                
                var addedTransaction = await _transactionRepository.Add(newTransaction);
                
                if (addedTransaction == null)
                {
                    throw new InvalidOperationException("Failed to create transaction.");
                }
                var transactionDto = _mapper.Map<TransactionDto>(addedTransaction);
                return transactionDto; 
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: CreateTransactionHandler Method: Handle()");
                throw; 
            }
        }
    }
}
