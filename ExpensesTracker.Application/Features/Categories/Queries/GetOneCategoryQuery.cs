using ExpensesTracker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.Categories.Queries
{
    public class GetOneCategoryQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; } 
    }
}
