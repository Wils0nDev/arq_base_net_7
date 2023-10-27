using BaseArchitecture.Domain;

namespace BaseArchitecture.Application.Interfaces.Repositories
{

    /// <summary>
    /// Genera los metodos genéricos "create, delete, update" de cualquier entidad.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, IGenerateEntity<TEntity>
    {

        /// <summary>
        /// Crea un nuevo registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Create(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina un registro enviando una entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Delete(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(TEntity entity, CancellationToken cancellationToken = default);

    }

}
