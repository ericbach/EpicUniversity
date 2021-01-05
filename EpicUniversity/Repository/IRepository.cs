using System.Collections.Generic;

namespace EpicUniversity.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);               // Course Get(long id);
        ICollection<TEntity> GetAll();      // ICollection<Course> GetAll();
    }
}