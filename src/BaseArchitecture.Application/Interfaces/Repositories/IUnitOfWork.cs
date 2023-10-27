using BaseArchitecture.Domain;
using BaseArchitecture.Domain.Entities;
using EFCore.BulkExtensions;
using System.Data.Common;


namespace BaseArchitecture.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {

        #region Entidades mapeadas al contexto

        IGenericRepository<MasterTable> MasterTables { get; }

        IGenericRepository<Person> Persons { get; }

        IGenericRepository<PersonFile> PersonFiles { get; }

        IGenericRepository<AttachedFile> AttachedFiles { get; }


        #endregion


        #region Auditoria y log

        IGenericRepository<LogDb> LogDbs { get; }
        IGenericRepository<LogJob> LogJobs { get; }
        IGenericRepository<AuditEndpoint> AuditEndpoints { get; }
        IGenericRepository<AuditHttp> AuditHttps { get; }

        #endregion


        #region Funciones y metodos públicos


        /// <summary>
        /// Proceso de registros masivos a base de datos, no requiere de ejecutar SaveChanges();
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad adjuntada al contexto.</typeparam>
        /// <param name="entities">Lista de datos a insertar en masivo.</param>
        /// <param name="bulkConfig">Configuración de proceso masivo</param>
        void BulkInsert<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null) where TEntity : class, IGenerateEntity<TEntity>;

        /// <summary>
        /// Proceso de registros masivos a base de datos en asíncrono, no requiere de ejecutar SaveChangesAsync();
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad adjuntada al contexto.</typeparam>
        /// <param name="entities">Lista de datos a insertar en masivo.</param>
        /// <param name="bulkConfig">Configuración de proceso masivo</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkInsertAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;


        /// <summary>
        /// Proceso masivos de actualización a base de datos, no requiere de ejecutar SaveChanges();
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="bulkConfig"></param>
        void BulkUpdate<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null) where TEntity : class, IGenerateEntity<TEntity>;


        /// <summary>
        /// Proceso masivo de actualización a base de datos en asíncrono, no requiere de ejecutar SaveChangesAsync();
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad adjuntada al contexto.</typeparam>
        /// <param name="entities">Lista de datos a insertar en masivo.</param>
        /// <param name="bulkConfig">Configuración de proceso masivo</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkUpdateAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;



        /// <summary>
        /// Proceso de limpieza de tabla en Base de datos.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad adjuntada al contexto.</typeparam>
        /// <param name="type"></param>
        void Truncate<TEntity>(Type type = null) where TEntity : class, IGenerateEntity<TEntity>;

        /// <summary>
        /// Proceso de limpieza de tabla en Base de datos en modo asíncrono.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad adjuntada al contexto.</typeparam>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task TruncateAsync<TEntity>(Type type = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;




        /// <summary>
        /// Guarda los cambios realizados del contexto a la Base de Datos
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Guarda los cambios realizados en forma asíncrona del contexto a la Base de Datos.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicia un ambiente de transacción  
        /// </summary>
        /// <returns></returns>
        DbTransaction Transaction();

        /// <summary>
        /// Inicia un ambiente de transacción en asíncrono
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DbTransaction> TransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adjunta un ambiente de transacción al contexto
        /// </summary>
        /// <param name="dbTransaction"></param>
        void UseTransaction(DbTransaction dbTransaction);

        /// <summary>
        /// Adjunta un ambiente de transacción al contexto en asíncrono.
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UseTransactionAsync(DbTransaction dbTransaction, CancellationToken cancellationToken = default);


        #endregion

    }
}