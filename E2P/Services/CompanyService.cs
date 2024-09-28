using E2P.Models;
using E2P.Models.SearchFilters;
using E2P.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E2P.Services
{
    public class CompanyService : IService<Company, CompanySearchFilters>
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateAsync(Company entity)
        {
            _context.Companies.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetByFiltersAsync(CompanySearchFilters filters)
        {
            var result = await _context.Companies.FindAsync(filters);
            if (result == null)
            {
                throw new Exception("No entry was found");
            }

            return result;
        }

        public async Task<bool> UpdateAsync(Company entity)
        {
            _context.Companies.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Companies.FindAsync(id);
            if (entity == null) return false;

            _context.Companies.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Company>> FindAsync(Expression<Func<Company, bool>> predicate)
        {
            return await _context.Companies.Where(predicate).ToListAsync();
        }
    }
}
