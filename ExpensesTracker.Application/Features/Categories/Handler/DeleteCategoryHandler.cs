using AutoMapper;
using ExpensesTracker.Application.Features.Categories.Commands;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _categoryRepository.Delete(request.Id); 
            }catch(Exception ex)
            {
                _logger.Error(ex, "Class: CreateCategoryHandler Method: Handle()");
                throw;
            }
        }
    }
}
