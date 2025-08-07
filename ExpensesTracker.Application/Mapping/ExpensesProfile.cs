using AutoMapper;
using Expenses_Tracker;
using ExpensesTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Mapping
{
    internal class ExpensesProfile : Profile
    {
        public ExpensesProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Transaction, TransactionDto>()
            .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dst => dst.TypeName, opt => opt.MapFrom(src => src.Type.Type));
            CreateMap<TransactionType, TransactionTypeDto>();

        }
    }
}
