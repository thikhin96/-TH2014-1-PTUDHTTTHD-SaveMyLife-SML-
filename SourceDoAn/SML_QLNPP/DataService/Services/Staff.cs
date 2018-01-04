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
                else
                {
                    logger.Info("Start GetAll Staff....");
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End GetAll Staff....");
                return null;
            }
        }
    }
}
