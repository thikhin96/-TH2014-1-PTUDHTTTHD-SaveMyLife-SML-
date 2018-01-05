using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using DataModel.Interfaces;
using DataModel;
using DataService.Interfaces;

namespace DataService.Services
{
    public class StaffService: IStaffService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork uow;
        IRepository<Staff> repo_staff;

        public StaffService(IUnitOfWork _unitOfWork)
        {
            uow = _unitOfWork;
            repo_staff = uow.Repository<Staff>();
        }

        public Staff GetByAccount(string account)
        {
            logger.Info("Start to get info of staff...");
            Staff emp = repo_staff.Get(x => x.account == account);
            logger.Info("End");
            return emp;
        }
    }

}
