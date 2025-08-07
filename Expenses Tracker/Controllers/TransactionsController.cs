using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Transactions.Commands;
using ExpensesTracker.Application.Features.Transactions.Queries;
using ExpensesTracker.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;


namespace Expenses_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var transactions=  await _mediator.Send(new GetAllTransactionsQuery());
            var UserId = getUserId(); 
           var GetAllTransactionDto = await _mediator.Send(new GetAllTransactionsQuery()
           {
               UserId = UserId
           });
            if (GetAllTransactionDto==null)
                return ApiResponse<GetAllTransactionsDTO>.FailureResponse("There is no transactions Found !");
            return ApiResponse<GetAllTransactionsDTO>.SuccessResponse(GetAllTransactionDto, "Transactions loaded Successfully !");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction  = await _mediator.Send(new GetOneTransactionQuery()
            {
                Id = id
            });

            if (transaction==null)
                return ApiResponse<TransactionDto>.FailureResponse("There is no transactions Found !");
            return ApiResponse<TransactionDto>.SuccessResponse(transaction, "Transaction loaded Successfully !");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionDto transactionDto)
        {
            var createdTransaction =await _mediator.Send(new CreateTransactionCommand
            {
                Amount = transactionDto.Amount,
                Date = DateTime.Now,
                Note = transactionDto.Note,
                CategoryId = transactionDto.CategoryId,
                TypeId = transactionDto.TypeId,
                UserId = getUserId()
                
            });

            if (createdTransaction==null)
                return ApiResponse<TransactionDto>.FailureResponse("Faild to create transaction!");
            return ApiResponse<TransactionDto>.SuccessResponse(createdTransaction, "Transactions Created Successfully !");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransactionDto Dto)
        {
            var updatedTransaction = await _mediator.Send(new UpdateTransactionCommand()
            {
                Amount = Dto.Amount, 
                Date = DateTime.Now,
                Note = Dto.Note,
                CategoryId = Dto.CategoryId,
                TypeId = Dto.TypeId,
                UserId = getUserId()

            });
            if (updatedTransaction == null)
                return ApiResponse<TransactionDto>.FailureResponse("Faild to create transaction!");
            return ApiResponse<TransactionDto>.SuccessResponse(updatedTransaction, "Transactions Created Successfully !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status  = await _mediator.Send(new DeleteTransactionCommand()
            {
                Id = id
            });
            if (status)
                return ApiResponse<bool>.FailureResponse("Faild to delete transaction!");
            return ApiResponse<bool>.SuccessResponse(status, "Transactions Deleted Successfully !");

        }

        private string getUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
