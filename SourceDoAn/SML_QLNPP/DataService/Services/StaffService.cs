using DataModel.Interfaces;
using DataService.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public StaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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