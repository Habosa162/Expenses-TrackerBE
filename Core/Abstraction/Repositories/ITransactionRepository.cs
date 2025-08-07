using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenses_Tracker;

namespace ExpensesTracker.Core.Abstraction.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public IQueryable<Transaction> GetQueryableTransactions(); 
    }
}
