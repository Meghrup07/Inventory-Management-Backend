using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GenericRepository<T>(DataContext context) : IGenericRepository<T> where T : class
    {
        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Entry(entity).State = EntityState.Modified;
        }
    }
}