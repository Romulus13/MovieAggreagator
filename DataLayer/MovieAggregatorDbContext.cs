
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataLayer.Configurations;
using System.Data;

namespace DataLayer
{
    public class MovieAggregatorDbContext : DbContext, IUnitOfWork
    {

        private readonly ILogger _logger;

        public MovieAggregatorDbContext(DbContextOptions<MovieAggregatorDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _logger = loggerFactory.CreateLogger("logs");
        }

        public IGenericRepository<ImdbStoredResult> ImdbResults => GetRepository<ImdbStoredResult>();
        public IGenericRepository<YoutubeStoredResult> YoutubeResults => GetRepository<YoutubeStoredResult>();


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImdbResultConfiguration());
            modelBuilder.ApplyConfiguration(new YoutubeResultConfiguration());
        }
        #endregion


        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            var returnInt = await SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return returnInt;
        }


        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(this, _logger);
        }

        public void DoInTransaction(Action<IUnitOfWork> action, IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            using (var transaction = Database.BeginTransaction(level))
            {
                try
                {
                    action(this);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public void Detach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Detached;
        }
    }
}