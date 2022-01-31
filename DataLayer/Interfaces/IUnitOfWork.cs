
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;


namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<YoutubeStoredResult> YoutubeResults { get; }

        IGenericRepository<ImdbStoredResult> ImdbResults { get; }
   
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));

        IGenericRepository<T> GetRepository<T>() where T: class;

        void DoInTransaction(Action<IUnitOfWork> action, IsolationLevel level = IsolationLevel.ReadCommitted);

        void Detach<T>(T entity) where T : class;

        int SaveChanges();
    }
}
