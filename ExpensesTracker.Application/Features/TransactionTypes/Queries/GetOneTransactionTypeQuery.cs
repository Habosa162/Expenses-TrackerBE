using AutoMapper;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Application.Features.TransactionTypes.Queries
{
    public class GetOneTransactionTypeQuery : IRequest<TransactionTypeDto>
    {
        public int Id { get; set; }
    }
}
