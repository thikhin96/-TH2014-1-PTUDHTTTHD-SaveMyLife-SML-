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

        public Account Login(string username, string password)
        {
            IRepository<Account> repository = _unitOfWork.Repository<Account>();
            return repository.Get(a => a.UserName == username && a.Password == password);
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

        public Account Get(string username)
        {
            IRepository<Account> repository = _unitOfWork.Repository<Account>();
            return repository.Get(a => a.UserName == username);
        }
    }
}
