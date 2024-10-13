using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;


namespace Bookstore.Infrastructure.Repositories
{
	public class UnitOfWork(BookManagementDbContext dbContext) : IUnitOfWork
	{
		private bool _disposed = false;
		private readonly BookManagementDbContext _context = dbContext;
		public void BeginTransaction()
		{
			_context.Database.BeginTransaction();
		}

		public void CommitTransaction()
		{
			_context.Database.CommitTransaction();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
                if (disposing)
                {
					_context.Dispose();
                }
            }
			_disposed = true;
		}

		public IGenericRepository<T> GetRepository<T>() where T : class
		{
			return new GenericRepository<T>(_context);
		}

		public void RollBack()
		{
			_context.Database.RollbackTransaction();
		}

		public void Save()
		{
			_context.SaveChanges();
		}
        public async Task SaveChangeAsync()
        {
			await _context.SaveChangesAsync();	
        }
    }
}
