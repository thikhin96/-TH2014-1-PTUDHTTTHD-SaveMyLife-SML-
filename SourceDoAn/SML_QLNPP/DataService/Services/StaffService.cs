using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace DataService.Services
{
    public class StaffService: IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Staff> _staffRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public StaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = _unitOfWork.Repository<Staff>();
        }
        public List<Staff> getAll()
        {
            try
            {
                var staff = _staffRepository.GetAll().ToList();
                return staff;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Staff getStaff(int id)
        {
            try
            {
                var staff = _staffRepository.Get(x=> x.idStaff == id);
                return staff;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public IList<Staff> GetAll()
        {
            try
            {
                logger.Info("Start GetAll Staff....");
                IRepository<Staff> repository = _unitOfWork.Repository<Staff>();
                var result = repository.GetAll();
                if (result != null)
                {
                    logger.Info("Start GetAll Staff....");
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                logger.Info("End GetAll Staff....");
            }
            return null;
        }
        public Staff GetByAccount(string Account)
        {
            Staff staff = null;
            logger.Info("Start Get Staff by username....");
            try
            {
                IRepository<Staff> repository = _unitOfWork.Repository<Staff>();
                staff = repository.Get(a =>a.account == Account);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
            }
            logger.Info("End Get Staff by username....");
            return staff;
        }
    }
}
