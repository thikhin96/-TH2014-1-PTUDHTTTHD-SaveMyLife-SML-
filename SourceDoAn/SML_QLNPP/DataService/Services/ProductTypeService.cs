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
        public class ProductTypeService : IProductTypeService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRepository<ProductType> _productTypeRepository;
            ILogger logger = LogManager.GetCurrentClassLogger();
            /// <summary>
            /// Hàm khởi tạo
            /// </summary>
            /// <param name="unitOfWork"></param>
            public ProductTypeService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _productTypeRepository = _unitOfWork.Repository<ProductType>();
            }
            /// <summary>
            /// Hàm lấy tất cả các loại sản phẩm
            /// </summary>
            /// <returns></returns>
            public List<ProductType> GetAllProductType()
            {
                try
                {
                    logger.Info("Start audit get all product types!");
                    var types = _productTypeRepository.GetAll().ToList();
                    logger.Info("End audit get all product types!");
                    return types;
                }
                catch (Exception ex)
                {
                    logger.Info("Error audit get all product types: " + ex.Message);
                    throw;
                }
            }
        }
    }

