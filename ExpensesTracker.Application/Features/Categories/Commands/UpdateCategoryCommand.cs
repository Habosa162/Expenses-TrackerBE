using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; } = string.Empty;
        public bool? IsDefault { get; set; } = false;
    }
}
