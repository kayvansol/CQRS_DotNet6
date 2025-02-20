using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Domain.Entities;
using Store.Infra.Sql.Context;
using System.Linq.Expressions;

namespace Store.Infra.Sql.Repositories
{
    public class Repository<Entity, Key> : IDisposable, IRepository<Entity, Key>
        where Entity : BaseEntity<Key>
        where Key : notnull
    {
        private readonly StoreContext _context;
        private DbSet<Entity> _dbSet;

        public Repository(StoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public async Task<Entity> Add(Entity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddRange(List<Entity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public Task CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void Delete(Entity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<Entity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<bool> Exist(Key id)
        {
            return true;
        }

        public async Task<Entity?> Get(Key id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<Entity> GetAll(Expression<Func<Entity, bool>>? predict = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>>? orderBy = null, bool disableTracking = true, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>? Includes = null)
        {
            var query = _dbSet.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if(Includes is not null)
                query = Includes(query);

            if(predict is not null) query = query.Where(predict);

            if(orderBy is not null)
                return orderBy(query);

            return query;

        }

        public async Task RollBackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Entity Update(Entity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
