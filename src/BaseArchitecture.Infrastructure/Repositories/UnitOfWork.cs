using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.Persistence;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace BaseArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly BaseDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool disposedValue;

        public UnitOfWork(BaseDbContext Context, IServiceProvider serviceProvider)
        {
            this._context = Context;
            this._serviceProvider = serviceProvider;
        }

        #region Entidades mapeadas al contexto

        public IGenericRepository<MasterTable> MasterTables =>
              this._serviceProvider.GetRequiredService<IGenericRepository<MasterTable>>();

        public IGenericRepository<Person> Persons =>
             this._serviceProvider.GetRequiredService<IGenericRepository<Person>>();

        public IGenericRepository<PersonFile> PersonFiles =>
              this._serviceProvider.GetRequiredService<IGenericRepository<PersonFile>>();

        public IGenericRepository<AttachedFile> AttachedFiles =>
              this._serviceProvider.GetRequiredService<IGenericRepository<AttachedFile>>();

        #endregion


        #region Auditoria y log

        public IGenericRepository<LogDb> LogDbs =>
          this._serviceProvider.GetRequiredService<IGenericRepository<LogDb>>();

        public IGenericRepository<LogJob> LogJobs =>
             this._serviceProvider.GetRequiredService<IGenericRepository<LogJob>>();
        public IGenericRepository<AuditEndpoint> AuditEndpoints =>
           this._serviceProvider.GetRequiredService<IGenericRepository<AuditEndpoint>>();

        public IGenericRepository<AuditHttp> AuditHttps =>
            this._serviceProvider.GetRequiredService<IGenericRepository<AuditHttp>>();

        #endregion


        #region Funciones y metodos públicos


        public void BulkInsert<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null) where TEntity : class, IGenerateEntity<TEntity>
        {
            this._context.BulkInsert(entities, bulkConfig);
        }

        public Task BulkInsertAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>
        {
            return this._context.BulkInsertAsync(entities, bulkConfig, cancellationToken: cancellationToken);
        }


        void IUnitOfWork.BulkUpdate<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig)
        {
            this._context.BulkUpdate(entities, bulkConfig);
        }

        Task IUnitOfWork.BulkUpdateAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig, CancellationToken cancellationToken)
        {
            return this._context.BulkUpdateAsync(entities, bulkConfig, cancellationToken: cancellationToken);
        }

        public void Truncate<TEntity>(Type type = null) where TEntity : class, IGenerateEntity<TEntity>
        {
            this._context.Truncate<TEntity>(type);
        }

        public Task TruncateAsync<TEntity>(Type type = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>
        {
            return this._context.TruncateAsync<TEntity>(type, cancellationToken: cancellationToken);
        }




        public DbTransaction Transaction()
        {
            return _context.Database.BeginTransaction().GetDbTransaction();
        }


        public async Task<DbTransaction> TransactionAsync(CancellationToken cancellationToken = default)
        {
            return (await _context.Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
        }


        public void UseTransaction(DbTransaction dbTransaction)
        {
            _context.Database.UseTransaction(dbTransaction);
        }


        public async Task UseTransactionAsync(DbTransaction dbTransaction, CancellationToken cancellationToken = default)
        {
            await _context.Database.UseTransactionAsync(dbTransaction, cancellationToken);
        }


        public int SaveChanges()
        {
            try
            {
                var vResult = this._context.SaveChanges();
                return vResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Record does not exist in the database");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var vResult = await this._context.SaveChangesAsync(cancellationToken);
                return vResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Record does not exist in the database");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                    _context.Dispose();
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~UnitOfWork()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

       

        #endregion


    }

}
