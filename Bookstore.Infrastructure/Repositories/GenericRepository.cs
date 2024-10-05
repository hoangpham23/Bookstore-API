using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BookManagementDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(BookManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

		public IQueryable<T> Entities => _context.Set<T>();

		public async Task DeleteAsync(object id)
		{
			T entity = await _dbSet.FindAsync(id) ?? throw new Exception();
			_dbSet.Remove(entity);
		}

		public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(predicate);
		}

		public async Task<IList<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<BasePaginatedList<T>> GetPagging(IQueryable<T> query, int index, int pageSize)
		{
			query = query.AsNoTracking();
			int count = await query.CountAsync();
			IReadOnlyCollection<T> items = await query.Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
			return new BasePaginatedList<T>(items, count, index, pageSize);
		}

		public async Task InsertAsync(T obj)
		{
			await _dbSet.AddAsync(obj);
		}

		public Task UpdateAsync(T obj)
		{
			return Task.FromResult(_dbSet.Update(obj));
		}
	}
}
