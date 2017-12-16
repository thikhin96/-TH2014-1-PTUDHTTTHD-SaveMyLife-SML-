using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Interfaces;
using DataService.Interfaces;
using DataModel;
using NLog;
namespace DataService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public AccountService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public bool CreateAccount(Account acc)
        {
            throw new NotImplementedException();
        }

        public bool IncurredAccount(string username, byte typeOfUser)
        {
            throw new NotImplementedException();
        }

        public bool LockAccount(int idAccount, string reason)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Hàm kiểm tra đăng nhập theo username và password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Login(string username, string password)
        {
            IRepository<Account> repository = _unitOfWork.Repository<Account>();
            logger.Info("Start get login");
            Account result;
            try
            {
                result = repository.Get(a => a.UserName == username && a.Password == password);
                logger.Info("Completed get login");

            }
            catch (Exception ex)
            {
                logger.Info("Error get login: " + ex.Message);
                result = null;
            }
            return result;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public IList<Account> SearchAccount(int idAccount)
        {
            throw new NotImplementedException();
        }

        public void writeLogUser(int idUser, bool status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin user theo tên người dùng để ghi log
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Account Get(string username)
        {
            IRepository<Account> repository = _unitOfWork.Repository<Account>();
            Account result;
            logger.Info("Start get account to write log");
            try
            {
                result = repository.Get(a => a.UserName == username);
                logger.Info("Completed get account to write log");

            }
            catch (Exception ex)
            {
                logger.Info("Error get account to write log: " + ex.Message);
                result = null;
            }
            return result;
        }
    }
}
