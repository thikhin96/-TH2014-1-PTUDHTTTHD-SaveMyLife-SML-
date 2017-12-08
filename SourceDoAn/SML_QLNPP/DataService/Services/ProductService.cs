using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product GetProduct(int idp)
        {
            IRepository<Product> repository = _unitOfWork.Repository<Product>();
            var result = repository.Get(a => a.IdProduct == idp);
            return result != null ? result : null;
        }

        public List<Product> Search(string keyWord)
        {
            try
            {
                IRepository<Product> repository = _unitOfWork.Repository<Product>();
                if (keyWord=="")
                {
                    return repository.GetAll().ToList();
                }
                else
                {
                    return repository.GetAll( a => (a.ProductName.Contains(keyWord) 
                                                   || a.IdProduct.ToString().CompareTo(keyWord)==0)).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
