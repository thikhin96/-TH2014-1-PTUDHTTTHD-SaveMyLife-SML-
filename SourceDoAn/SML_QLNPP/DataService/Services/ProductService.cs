using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
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
    }
}
