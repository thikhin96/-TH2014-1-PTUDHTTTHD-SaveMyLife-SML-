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
        ILogger logger = LogManager.GetCurrentClassLogger();
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
                logger.Info("Start audit get all products not disabled");
                var products = _productRepository.GetAll(p => p.IsDisabled == false).ToList();
                logger.Info("End audit get all products not disabled");
                return products;
            }
            catch (Exception ex)
            {
                logger.Info("Error audit get all products not disabled: " + ex.Message);
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
                logger.Info("Start audit search product by keyword");
                IRepository<Product> repository = _unitOfWork.Repository<Product>();
                if (keyWord=="")
                {
                    logger.Info("End audit search product by keyword");
                    return repository.GetAll().ToList();
                }
                else
                {
                    logger.Info("End audit search product by keyword");
                    return repository.GetAll( a => (a.ProductName.Contains(keyWord) 
                                                   || a.IdProduct.ToString().CompareTo(keyWord)==0)).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Info("Error audit search product by keyword: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Hàm generate mã sản phẩm
        /// </summary>
        /// <returns></returns>
        public int GenerateProductId()
        {
            logger.Info("Start audit generate product's id");
            var latestProduct = _productRepository.GetAll().OrderByDescending(x => x.IdProduct).FirstOrDefault();
            if (latestProduct != null)
            {
                logger.Info("End audit generate product's id");
                return latestProduct.IdProduct + 1;
            }

            else
            {
                logger.Info("End audit generate product's id");
                return 1;
            }
               
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
                logger.Info("Start audit create product");
                _productRepository.Add(product);
                _unitOfWork.SaveChange();
                logger.Info("End audit create product");
                return "ok";
            }
             catch
            {
                logger.Info("Error audit create product");
                return "Tạo sản phẩm thất bại";
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
                logger.Info("Start audit update product");
                _productRepository.Update(product);
                _unitOfWork.SaveChange();
                logger.Info("End audit create product");
                return "ok";
            }
            catch
            {
                logger.Info("Error audit create product");
                return "Cập nhật sản phẩm thất bại";
            }
        }

    }
}
