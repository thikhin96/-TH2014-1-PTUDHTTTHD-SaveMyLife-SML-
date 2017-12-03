using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace DataService.Interfaces
{
    public interface IAccountService
    {
        bool Login(string username,string password);
        void Logout();
        IList<Account> SearchAccount(int idAccount);
        bool LockAccount(int idAccount,string reason);
        bool CreateAccount(Account acc);
        bool IncurredAccount(string username, byte typeOfUser);
        void writeLogUser(int idUser, bool status);
    }
}
