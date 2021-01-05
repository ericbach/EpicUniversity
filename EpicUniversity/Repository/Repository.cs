using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class // == Unit of Work
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>(); // context.Set<Course>();
        }

        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }
    }
}
