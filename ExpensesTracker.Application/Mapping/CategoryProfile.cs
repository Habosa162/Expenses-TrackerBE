using AutoMapper;
using Expenses_Tracker;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Features.Categories.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
