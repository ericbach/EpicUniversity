using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EpicUniversity.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);               // Course Get(long id);
        Task<TEntity> GetAsync(long id);
        ICollection<TEntity> GetAll();      // ICollection<Course> GetAll();
        Task<ICollection<TEntity>> GetAllAsync();

        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        void Add(TEntity entity);           // INSERT new entity
        Task AddAsync(TEntity entity);
        void AddRange(ICollection<TEntity> entities);
        Task AddRangeAsync(ICollection<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);

        void Remove(long id);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}