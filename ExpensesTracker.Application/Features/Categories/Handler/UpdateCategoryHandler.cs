using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Categories.Commands;
using ExpensesTracker.Core.Abstraction.Repositories;
using MediatR;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Categories.Handler
{
    internal class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateCategoryHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _categoryRepository.Get((int)request.Id);
                if (existingCategory is null)
                {
                    throw new KeyNotFoundException($"Category with Id {request.Id} does not exist.");
                }

                if (await _categoryRepository.Exists(request.Name))
                {
                    throw new InvalidOperationException($"Category name '{request.Name}' is already in use.");
                }

                _mapper.Map(request, existingCategory);

                await _categoryRepository.Update(existingCategory);

                return _mapper.Map<CategoryDto>(existingCategory);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Class: UpdateCategoryHandler Method: Handle()");
                throw;
            }
        }
    }
}
