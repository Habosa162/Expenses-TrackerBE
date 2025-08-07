using AutoMapper;
using Expenses_Tracker;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Categories.Commands;
using ExpensesTracker.Core.Abstraction.Repositories;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Categories.Handler
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var categoryDb = await _categoryRepository.GetByName(request.Name); 
                if (categoryDb != null)
                {
                    throw new InvalidOperationException($"Category with name '{request.Name}' already exists.");
                }

                var category = _mapper.Map<Category>(request);

                var createdCategory = await _categoryRepository.Add(category);
                if (createdCategory == null)
                {
                    throw new InvalidOperationException("Failed to create category.");
                }

                return _mapper.Map<CategoryDto>(createdCategory);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Class: CreateCategoryHandler Method: Handle()");
                throw; 
            }
        }
    }
}
