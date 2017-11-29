using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate = null);
        TEntity Get(Func<TEntity, bool> predicate);
        void Add(TEntity entity);
        void Attach(TEntity entity);
        void Delete(TEntity entity);
    }
}
