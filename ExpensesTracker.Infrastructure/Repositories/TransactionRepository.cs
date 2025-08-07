using Expenses_Tracker;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository

    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> Add(Transaction entity)
        {
            try
            {
                var addedTransaction = await _context.Transactions.AddAsync(entity);
                if ((await _context.SaveChangesAsync() > 0))
                {
                    return (addedTransaction != null) ? addedTransaction.Entity : null;
                }
                return null;
            }
            catch(Exception ex)
            {
                Log.Error($"Class :: TransactionRepository Method :: Add() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
                if (transaction == null) return false;
                _context.Transactions.Remove(transaction);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionRepository || Method :: Delete() ````````` Exception::{ex} ");
                throw;
            }
        }

        public Task<bool> Exists(object entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> Get(int id)
        {
            try
            {
                return await _context.Transactions
                    .Include(t => t.Category)
                    .Include(t => t.Type)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch(Exception ex)
            {
                Log.Error($"Class :: TransactionRepository || Method :: Get() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            try
            {
                return await _context.Transactions
                    .Include(t => t.Category)
                    .Include(t => t.Type)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionRepository || Method :: GetAll() ````````` Exception::{ex} ");
                throw;
            }
        }

        public IQueryable<Transaction> GetQueryableTransactions()
        {
            try
            {
                return _context.Transactions
                    .Include(t => t.Category)
                    .Include(t => t.Type) ; 
            }catch(Exception ex)
            {
                Log.Error($"Class :: TransactionRepository || Method :: GetQueryableTransactions() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<Transaction> Update(Transaction entity)
        {
            try
            {
                _context.Transactions.Update(entity); 
                if(await _context.SaveChangesAsync() > 0) return entity;
                return null; 
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionRepository || Method :: Update() ````````` Exception::{ex} ");
                throw;
            }
        }
    }
}
