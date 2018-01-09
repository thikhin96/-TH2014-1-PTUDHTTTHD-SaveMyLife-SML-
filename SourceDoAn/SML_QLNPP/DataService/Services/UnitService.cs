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
    public class UnitService: IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Unit> _unitRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitRepository = _unitOfWork.Repository<Unit>();
        }
        /// <summary>
        /// Hàm lấy tất cả các loại đơn vị tính
        /// </summary>
        /// <returns></returns>
        public List<Unit> GetAllUnit()
        {
            try
            {
                logger.Info("Start audit get all units!");
                var units = _unitRepository.GetAll().ToList();
                logger.Info("End audit get all units!");
                return units;
            }
            catch (Exception ex)
            {
                logger.Info("Error audit get all units: " + ex.Message);
                throw;
            }
        }
    }
}
