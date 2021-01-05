using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicUniversity.Data;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class // == Unit of Work
    {
        internal DbContext Context;
        //private UniversityContext UniversityContext;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            //UniversityContext = universityContext;
            DbSet = context.Set<TEntity>(); // context.Set<Course>();
        }

        public List<TEntity> GetAll()
        {
            //return UniversityContext.Courses.ToList();
            return DbSet.ToList();
        }
    }
}
