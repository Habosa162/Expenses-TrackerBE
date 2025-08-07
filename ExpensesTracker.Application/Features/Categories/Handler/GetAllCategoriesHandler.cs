using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Categories.Queries;
using ExpensesTracker.Core.Abstraction.Repositories;
using MediatR;
using Serilog; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Categories.Handler
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCategoriesHandler(
                ICategoryRepository categoryRepository,
                IMapper mapper,
                ILogger logger
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoryRepository.GetAll();
                if (categories == null || !categories.Any())
                {
                    _logger.Warning("No categories found.");
                    return Enumerable.Empty<CategoryDto>();
                }
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                if (categoryDtos == null || !categoryDtos.Any())
                {
                    _logger.Warning("Mapping Categories to CategoryDto returned null or empty.");
                    return Enumerable.Empty<CategoryDto>();
                }
                return categoryDtos;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            void LogException(Exception ex)
            {
                _logger.Error($"Class :: GetOneCategoryHandler Method :: Handel() ````````` Exception::{ex} ");
            }
        }
    }
}
