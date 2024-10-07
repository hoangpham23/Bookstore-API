﻿namespace Bookstore.Domain.Abstractions
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> GetRepository<T>() where T : class;
		void Save();
		Task SaveChangeAsync();
		void BeginTransaction();
		void CommitTransaction();
		void RollBack();
	}
}
