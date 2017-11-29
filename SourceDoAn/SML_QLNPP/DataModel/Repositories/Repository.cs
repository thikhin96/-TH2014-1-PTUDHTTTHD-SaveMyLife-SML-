using DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DistributionManagementEntities _context;

        public Repository(DistributionManagementEntities context)
        {
            this._context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().First(predicate);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate = null)
        {
            if (predicate != null)
            {
                _context.Set<TEntity>().Where(predicate);
            }
            return _context.Set<TEntity>().AsEnumerable<TEntity>();
        }
    }
}
