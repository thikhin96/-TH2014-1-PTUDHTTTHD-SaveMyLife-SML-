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
    public class LogProductService: ILogProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Log_Product> _logProductRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public LogProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logProductRepository = _unitOfWork.Repository<Log_Product>();
        }
        /// <summary>
        /// Hàm ghi log sửa đơn giá sản phẩm
        /// </summary>
        /// <param name="log_p"></param>
        /// <returns></returns>
        public bool Add(Log_Product log_p)
        {
            try
            {
                logger.Info("Start audit update product's price");
                _logProductRepository.Add(log_p);
                _unitOfWork.SaveChange();
                logger.Info("End audit update product's price");
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("Error audit update product's price: " + ex.Message);
                return false;
            }
        }
    }
}
