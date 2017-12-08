using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity Get(Func<TEntity, bool> predicate);
        void Add(TEntity entity);
        void Attach(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
