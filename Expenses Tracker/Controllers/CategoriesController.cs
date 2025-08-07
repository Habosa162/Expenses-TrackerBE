using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Categories.Commands;
using ExpensesTracker.Application.Features.Categories.Queries;
using ExpensesTracker.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Reflection.Metadata.Ecma335;


namespace Expenses_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            if (categories.Any())
            {
                return ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(categories, "Categories Loaded Successfulley !"); 
            }
            return ApiResponse<IEnumerable<CategoryDto>>.FailureResponse("Faild To Load Categories !"); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _mediator.Send(new GetOneCategoryQuery()
            {
                Id = id
            }); 
            if(category == null)
            {
                return ApiResponse<CategoryDto>.NotFoundResponse($"There is no Category with ID : {id}"); 
            }
            return ApiResponse<CategoryDto>.SuccessResponse(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto Dto)
        {
           var createdCtageory =  await _mediator.Send(new CreateCategoryCommand()
            {
                IsDefault = Dto.IsDefault,
                Name = Dto.Name
            });
            if(createdCtageory == null)
            {
                return ApiResponse<CategoryDto>.FailureResponse("Faild to Create Category !"); 
            }
           return ApiResponse<CategoryDto>.SuccessResponse(createdCtageory,"Category Created Successfully !");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto Dto)
        {
            var updatedCategory = await _mediator.Send(new UpdateCategoryCommand()
            {
                Id = Dto.Id,
                IsDefault = Dto.IsDefault,
                Name = Dto.Name
            });
            if (updatedCategory == null)
                return ApiResponse<CategoryDto>.FailureResponse("Faild to Create Category !");
            return ApiResponse<CategoryDto>.SuccessResponse(updatedCategory, "Category Updated Successfully !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _mediator.Send(new DeleteCategoryCommand()
            {
                Id = id
            });
            if (!status)
                return ApiResponse<bool>.FailureResponse("Faild to Create Category !");
            return ApiResponse<bool>.SuccessResponse(status,"Category Deleted Successfully !");
        }
    }
}
