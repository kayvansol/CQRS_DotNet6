﻿
namespace Store.Infra.Sql.Repositories
{
    public interface IRepository<Entity, Key> where Entity : BaseEntity<Key> where Key : notnull
    {
        Task<Entity> Add(Entity entity);

        Task AddRange(List<Entity> entities);

        Entity Update(Entity entity);

        void Delete(Entity entity);

        void DeleteRange(List<Entity> entities);

        Task<Entity?> Get(Key id);

        IQueryable<Entity> GetAll(Expression<Func<Entity, bool>>?
            predicate = null,
            Func<IQueryable<Entity>, IOrderedQueryable<Entity>>? orderBy = null, bool disableTracking = true,
            Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>? Includes = null);

        Task<bool> Exist(Key id);

        Task SaveChanges();

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollBackTransaction();

    }
}
