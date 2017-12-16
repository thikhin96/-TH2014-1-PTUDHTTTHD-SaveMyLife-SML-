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
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public LogLoginService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Thêm Log đăng nhập khi người dùng đăng nhập
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool Add(Log_Login log)
        {
            IRepository<Log_Login> repository = _unitOfWork.Repository<Log_Login>();
            try
            {
                logger.Info("Start audit login");
                repository.Add(log);
                _unitOfWork.SaveChange();
                logger.Info("End audit login");
                return true;
            }
            catch(Exception ex)
            {
                logger.Info("Error audit login: " + ex.Message);
                return false;
            }
        }
    }
}
