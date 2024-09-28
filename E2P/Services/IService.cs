using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E2P.Services
{
    public interface IService<T, SF> where T : class
    {
        // Create
        Task<T> CreateAsync(T entity);

        // Read (get all entities)
        Task<IEnumerable<T>> GetAllAsync();

        // Read (get entity by id)
        Task<T> GetByFiltersAsync(SF filters);

        // Update
        Task<bool> UpdateAsync(T entity);

        // Delete
        Task<bool> DeleteAsync(int id);

        // Optional: Search by a condition
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
