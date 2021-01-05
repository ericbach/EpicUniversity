using System.Collections.Generic;

namespace EpicUniversity.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ICollection<TEntity> GetAll();
    }
}