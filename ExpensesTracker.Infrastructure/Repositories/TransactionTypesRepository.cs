using Expenses_Tracker;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Infrastructure.Repositories
{
    public class TransactionTypesRepository : ITransactionTypesRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionTypesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TransactionType> Add(TransactionType entity)
        {
            try
            {
                var Type = await _context.TransactionTypes.AddAsync(entity);
                if ((await _context.SaveChangesAsync() > 0))
                {
                    return (Type != null) ? Type.Entity : null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionTypesRepository Method :: Add() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Type = await _context.TransactionTypes.FirstOrDefaultAsync(t => t.Id == id);
                if (Type == null) return false;
                _context.TransactionTypes.Remove(Type);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionTypesRepository Method :: Delete() ````````` Exception::{ex} ");
                throw;
            }
        }

        public Task<bool> Exists(object entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionType> Get(int id)
        {
            try
            {
                return await _context.TransactionTypes
                     .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionTypesRepository Method :: Get() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<IEnumerable<TransactionType>> GetAll()
        {
            try
            {
                return await _context.TransactionTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionTypesRepository Method :: GetAll() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<TransactionType> Update(TransactionType entity)
        {
            try
            {
                _context.TransactionTypes.Update(entity);
                if ((await _context.SaveChangesAsync() > 0))
                {
                    return (entity != null) ? entity : null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: TransactionTypesRepository Method :: Update() ````````` Exception::{ex} ");
                throw;
            }
        }
    }
}
