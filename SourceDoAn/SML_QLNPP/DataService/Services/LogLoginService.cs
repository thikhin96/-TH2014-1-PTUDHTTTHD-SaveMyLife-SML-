using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using NLog;
namespace DataService
{
    public class LogLoginService : ILogLoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Log_Login> _LogLoginRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public LogLoginService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }

        public bool Add(Log_Login log)
        {
            IRepository<Log_Login> repository = _unitOfWork.Repository<Log_Login>();
            try
            {
                repository.Add(log);
                _unitOfWork.SaveChange();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
