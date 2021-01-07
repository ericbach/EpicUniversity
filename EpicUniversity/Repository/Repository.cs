using System.Collections.Generic;
using System.Linq;
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

        // SELECT * FROM WHERE Id = id
        public TEntity Get(long id)
        {
            return DbSet.Find(id);
        }

        // SELECT * FROM 
        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        // INSERT INTO 
        public void Add(TEntity entity)
        {
            if (entity == null) return;

            DbSet.Add(entity);
        }

        // INSERT INTO ... x 1000
        public void AddRange(ICollection<TEntity> entities)
        {
            if (entities == null) return;

            DbSet.AddRange(entities);
        }
    }
}
