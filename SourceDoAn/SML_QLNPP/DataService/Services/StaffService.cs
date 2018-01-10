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
            logger.Info("Start to get all staff");
            try
            {
                var staff = _staffRepository.GetAll().ToList();
                logger.Info("Status: Success");
                return staff;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return null;
            }
        }

        public Staff GetStaff(int id)
        {
            logger.Info("Start to get a staff");
            try
            {
                var staff = _staffRepository.Get(x=> x.idStaff == id);
                logger.Info("Status: Success");
                return staff;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return null;
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
