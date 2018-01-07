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

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.Repository<Product>();
        }

        /// <summary>
        /// Hàm lấy tất cả các sản phẩm 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Hàm lấy sản phẩm có mã là idp
        /// </summary>
        /// <param name="idp"></param>
        /// <returns></returns>
        public Product GetProduct(int idp)
        {
            IRepository<Product> repository = _unitOfWork.Repository<Product>();
            var result = repository.Get(a => a.IdProduct == idp);
            return result != null ? result : null;
        }

        /// <summary>
        /// Hàm tìm kiếm sản phẩm theo keyWord
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Hàm generate mã sản phẩm
        /// </summary>
        /// <returns></returns>
        public int GenerateProductId()
        {
            var latestProduct = _productRepository.GetAll().OrderByDescending(x => x.IdProduct).FirstOrDefault();
            if (latestProduct != null)
                return latestProduct.IdProduct + 1;
            else
                return 1;
        }

        /// <summary>
        /// Hàm thêm sản phẩm mới
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string CreateProduct(Product product)
        {
            try
            {
                _productRepository.Add(product);
                _unitOfWork.SaveChange();
                return "ok";
            }
             catch
            {
                return "Tạo sản phẩm thất bại";
                throw;
            }
        }

        /// <summary>
        /// Hàm chỉnh sửa sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string UpdateProduct(Product product)
        {
            try
            {
                _productRepository.Update(product);
                _unitOfWork.SaveChange();
                return "ok";
            }
            catch
            {
                return "Cập nhật sản phẩm thất bại";
                throw;
            }
        }

    }
}
