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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.Repository<Product>();
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                var products = _productRepository.GetAll(p => p.IsDisabled == false).ToList();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
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
