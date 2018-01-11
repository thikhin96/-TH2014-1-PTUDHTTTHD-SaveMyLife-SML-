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

        int GenerateUserId()
        {
            IRepository<Account> repo_Account = _unitOfWork.Repository<Account>();
            var latestAccount = repo_Account.GetAll().OrderByDescending(x => x.idUser).FirstOrDefault();
            if (latestAccount != null)
                return latestAccount.idUser + 1;
            else
                return 0;
        }

        public bool CreateAccount(string username, byte typeOfUser)
        {
            IRepository<Account> repo_Account = _unitOfWork.Repository<Account>();
            logger.Info("Start to create account...");
            try
            {
                Account _account = new Account();
                _account.idUser = GenerateUserId();
                _account.locked = false;
                _account.note = "Tạo mới";
                _account.dateCreate = DateTime.Now;
                _account.dateUpdate = DateTime.Now;
                _account.UserName = username;
                _account.Password = "123456789";
                _account.activated = true;
                _account.decentralization = typeOfUser;
                
                repo_Account.Add(_account);
                _unitOfWork.SaveChange();
                logger.Info("End: Successful...");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool UpdateStatus(string username, string reason, bool status)
        {
            IRepository<Account> repo_Account = _unitOfWork.Repository<Account>();
            logger.Info("Start to lock account...");
            Account _account;
            try
            {
                _account = Get(username);
                _account.locked = status;
                _account.note = reason;
                _account.dateUpdate = DateTime.Now;
                repo_Account.Update(_account);
                _unitOfWork.SaveChange();
                logger.Info("End: Successful...");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
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
                logger.Error("Error get login: " + ex.Message);
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
                logger.Error("Error get account to write log: " + ex.Message);
                result = null;
            }
            return result;
        }
    }
}
