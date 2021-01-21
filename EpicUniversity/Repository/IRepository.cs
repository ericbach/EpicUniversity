using System.Collections.Generic;

namespace EpicUniversity.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);               // Course Get(long id);
        ICollection<TEntity> GetAll();      // ICollection<Course> GetAll();

        void Add(TEntity entity);           // INSERT new entity
        void AddRange(ICollection<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);

        void Remove(long id);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);
    }
}