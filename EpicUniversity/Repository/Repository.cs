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

        public TEntity Get(long id)
        {
            return DbSet.Find(id);
        }

        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }
    }
}
