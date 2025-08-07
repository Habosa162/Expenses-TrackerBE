using Expenses_Tracker;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context; 
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<Category?> Add(Category entity)
        {
            try
            {
                var addedEntity  = await _context.Categories.AddAsync(entity);

                if ((await _context.SaveChangesAsync() > 0))
                {
                    return (addedEntity != null)  ? addedEntity.Entity : null  ;
                }

                return null;

            }
            catch (Exception ex) {
                Log.Error($"Class :: CategoryRepository Method :: Add() ````````` Exception::{ex} ");
                throw; 
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
                if (category == null) return false;
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Class :: CategoryRepository Method :: Delete() ````````` Exception::{ex} ");
                throw; 
            }
        }

        public async Task<bool> Exists(object entity)
        {
            try
            {
                return await _context.Categories.AnyAsync(c => c.Name == (string)entity || c.Id == (int)entity);
            }
            catch(Exception ex)
            {
                Log.Error($"Class :: CategoryRepository Method :: Exists() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<Category?> Get(int id)
        {
            try
            {
                if (id < 0) return null;
                return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch(Exception ex)
            {
                Log.Error($"Class :: CategoryRepository Method :: Get() ````````` Exception::{ex} ");
                throw;
            }
           
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                return await _context.Categories.ToListAsync();

            }
            catch (Exception ex)
            {
                Log.Error($"Class :: CategoryRepository Method :: GetAll() ````````` Exception::{ex} ");
                throw;
            }
        }

        public async Task<Category> GetByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower()); 
        }

        public async Task<Category?> Update(Category entity)
        {
            try
            {
                _context.Categories.Update(entity);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return entity;
                }
                return null;

            }
            catch (Exception ex)
            {
                Log.Error($"Class :: CategoryRepository Method :: Update() ````````` Exception::{ex} ");
                throw;
            }

        }
    }
}
