using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EpicUniversity.Models;
using Humanizer;
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

        public async Task<TEntity> GetAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }
        
        // SELECT * FROM TEntity
        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        // INSERT INTO TEntity VALUES(...)
        public void Add(TEntity entity)
        {
            if (entity == null) return;

            DbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) return;

            await DbSet.AddAsync(entity);
        }

        // INSERT INTO TEntity VALUES(...) * n
        public void AddRange(ICollection<TEntity> entities)
        {
            if (entities == null) return;

            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities)
        {
            if (entities == null) return;

            await DbSet.AddRangeAsync(entities);
        }

        // UPDATE TEntity SET column_name = values ...
        public virtual void Update(TEntity entity)
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

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
