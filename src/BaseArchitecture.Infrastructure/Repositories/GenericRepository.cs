using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Domain;
using BaseArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BaseArchitecture.Infrastructure.Repositories
{
    /// <summary>
    /// Implementa operaciones basicas de una entidad.
    /// </summary>
    /// <typeparam name="TEntity">Entidad que se encuentra asociada a la BD.</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
                              where TEntity : BaseEntity, IGenerateEntity<TEntity>
    {

        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;
        private readonly HeaderToken _headerToken;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BaseDbContext context, IMapper mapper, HeaderToken headerToken)
        {
            this._context = context;
            this._mapper = mapper;
            this._dbSet = context.Set<TEntity>();
            if (headerToken == null)            
                this._headerToken = new HeaderToken();  
            else
                this._headerToken = headerToken;
        }


        public IQueryable<TEntity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
        public void Create(TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.UserRecordCreation))            
                entity.UserRecordCreation = _headerToken.UserEdit;
            
            entity.RecordCreationDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Create(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.UserRecordCreation))
                entity.UserRecordCreation = _headerToken.UserEdit;

            entity.RecordCreationDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await this.CreateAsync(entity);
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }


        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Delete(entity);
        }

        public void Delete(params object[] id)
        {
            TEntity entity = this.Find(id);
            if (entity != null)
                this.Delete(entity);
        }

        public async Task DeleteAsync(params object[] id)
        {
            TEntity entity = await this.FindAsync(id);
            if (entity != null)
                this.Delete(entity);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            TEntity destination = entity.RecoverKey();
            _dbSet.Attach(destination);
            _context.Entry(destination).State = EntityState.Unchanged;
            _mapper.Map(entity, destination);
            destination.RecordEditDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(entity.UserEditRecord))
                destination.UserEditRecord = _headerToken.UserEdit; 

            var entry = _context.Entry(destination);
            return entry;
        }

    }
}
