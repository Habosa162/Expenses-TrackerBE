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
    public class GetOneCategoryHandler : IRequestHandler<GetOneCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger; 
        public GetOneCategoryHandler(ICategoryRepository categoryRepository,IMapper mapper, ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryDto> Handle(GetOneCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
              
                var category = await _categoryRepository.Get(request.Id);

                if (category is null)
                {
                    var ex = new KeyNotFoundException($"Category with Id {request.Id} does not exist.");
                    LogException(ex);
                    throw ex; 
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);
                if (categoryDto is null)
                {
                    var ex = new InvalidOperationException("Mapping Category -> CategoryDto returned null.");
                    LogException(ex);
                    throw ex;
                }

                return categoryDto;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw; 
            }
        }
        void LogException(Exception ex)
        {
            _logger.Error($"Class :: GetOneCategoryHandler Method :: Handel() ````````` Exception::{ex} ");
        }
    }
}
