using DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QL_NPPEntities _context;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private bool disposed = false;

        public UnitOfWork()
        {
            _context = new QL_NPPEntities();
        }



        public IRepository<T> Repository<T>() where T : class
        {
            IRepository<T> repo = null;
            if (repositories.ContainsKey(typeof(T)))
            {
                repo = repositories[typeof(T)] as IRepository<T>;
            }
            else
            {
                repo = new Repository<T>(_context);
                repositories.Add(typeof(T), repo);
            }
            return repo;
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (disposed == false)
            {
                if (disposing == true)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
