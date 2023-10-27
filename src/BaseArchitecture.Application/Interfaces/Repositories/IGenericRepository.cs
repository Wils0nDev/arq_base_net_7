using BaseArchitecture.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BaseArchitecture.Application.Interfaces.Repositories 
{
    /// <summary>
    /// Implementa operaciones basicas de una entidad.
    /// </summary>
    /// <typeparam name="TEntity">Entidad Be que se encuentra asociada a la BD.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, IGenerateEntity<TEntity>
    {

        /// <summary>
        /// Permite que la entidad pueda ser consultado con Linq o Lambda 
        /// sin ser almacenado en cache, esto permite que no se pueda 
        /// detectar los cambios que se realicen a los elementos de esta consulta.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsNoTracking();

        /// <summary>
        /// Permite que la entidad pueda ser consultado con Linq o Lambda.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();


        /// <summary>
        /// Permite agregar una entidad al contexto.
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);

        /// <summary>
        /// Permite agregar una entidad al contexto de forma asincrona.
        /// </summary>
        /// <param name="entity"></param>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Agrega una colección de entidades al contexto.
        /// </summary>
        /// <param name="entities"></param>
        void CreateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Agrega una colección de entidades al contexto de forma asíncrona.
        /// </summary>
        /// <param name="entities"></param>
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Busca con el ID un registro y lo marca para eliminarlo del contexto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(params object[] id);

        /// <summary>
        /// Adjunta una entidad y lo marca para ser eliminado del contexto.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Busca con el ID un registro de forma asíncrona y lo marca para eliminarlo del contexto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(params object[] id);

        /// <summary>
        /// Agrega una colección de entidades y lo marca para ser eliminados del contexto.
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Busca la Entidad con el ID el registro y lo adjunta al contexto para supervisar cambios. Si no encuentra registro, retorna null.
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Busca con el ID de forma asíncrona de una Entidad y lo adjunta al contexto para supervisar cambios. Si no encuentra registro, retorna null.
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Adjunta una entidad al contexto y marca solo las propiedades que fueron modificadas, 
        /// los valores por defecto de las propiedades no seran marcadas.
        /// Queda a la espera de indicar que propiedades adicionales serán marcados para modificar.
        /// El parámetro de entrada queda descartado.
        /// <para> var entry = unit.Alumno.Update(alumno); </para>
        /// <para> entry.Property(x => x.Apellido).IsModified = true;</para>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Update(TEntity entity);


    }
}