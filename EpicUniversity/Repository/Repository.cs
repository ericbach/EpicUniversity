using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity // == Unit of Work
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>(); // context.Set<Course>();
        }

        // SELECT * FROM TEntity WHERE Id = id
        public TEntity Get(long id)
        {
            return DbSet.Find(id);
        }

        // SELECT * FROM TEntity
        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        // INSERT INTO TEntity VALUES(...)
        public void Add(TEntity entity)
        {
            if (entity == null) return;

            DbSet.Add(entity);
        }

        // INSERT INTO TEntity VALUES(...) * n
        public void AddRange(ICollection<TEntity> entities)
        {
            if (entities == null) return;

            DbSet.AddRange(entities);
        }

        // UPDATE TEntity SET column_name = values ...
        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        // UPDATE TEntity SET column_name = values ... * n
        public void UpdateRange(ICollection<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        // DELETE FROM TEntity WHERE Id = id
        public void Remove(long id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        // DELETE FROM TEntity WHERE Id = id
        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        // DELETE FROM TEntity WHERE Id = id * n
        public void RemoveRange(ICollection<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
