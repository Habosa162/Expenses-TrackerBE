using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.TransactionTypes.Commands;
using ExpensesTracker.Application.Features.TransactionTypes.Queries;
using ExpensesTracker.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Expenses_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionTypesController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var types = await _mediator.Send(new GetAllTransactionTypeQuery());

            if (!types.Any())
                return ApiResponse<IEnumerable<TransactionTypeDto>>.FailureResponse("There is no transaction types Found !");
            return ApiResponse<IEnumerable<TransactionTypeDto>>.SuccessResponse(types, "Transaction Types loaded Successfully !");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var type = await _mediator.Send(new GetOneTransactionTypeQuery()
            {
                Id = id
            });
            if (type==null)
                return ApiResponse<TransactionTypeDto>.FailureResponse($"There is no transaction types Found using ID : {id}!");
            return ApiResponse<TransactionTypeDto>.SuccessResponse(type, "Transaction Type loaded Successfully !");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionTypeDto Dto)
        {
            var createdType = await _mediator.Send(new CreateTransactionTypeCommand()
            {
                Type = Dto.Type,
            });
            if (createdType == null)
                return ApiResponse<TransactionTypeDto>.FailureResponse($"Faild to create Transaction Type !");
            return ApiResponse<TransactionTypeDto>.SuccessResponse(createdType, "Transaction Type Created Successfully !");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransactionTypeDto Dto)
        {
            var updatedType = await _mediator.Send(new UpdateTransactionTypeCommand()
            {
                Id = (int)Dto.Id,
                Type = Dto.Type,
            });
            if (updatedType == null)
                return ApiResponse<TransactionTypeDto>.FailureResponse($"Faild to update Transaction Type !");
            return ApiResponse<TransactionTypeDto>.SuccessResponse(updatedType, "Transaction Type Updated Successfully !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete(int id)
        {
            var status = await _mediator.Send(new DeleteTransactionTypeCommand()
            {
                Id = id
            });
            if (!status)
                return ApiResponse<bool>.FailureResponse($"Faild to delete Transaction Type !");
            
            return ApiResponse<bool>.SuccessResponse(status, "Transaction Type Deleted Successfully !");
        }
    }
}
