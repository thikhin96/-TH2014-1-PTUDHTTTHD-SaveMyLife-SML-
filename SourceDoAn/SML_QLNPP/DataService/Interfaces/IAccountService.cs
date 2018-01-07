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
        Account Login(string username,string password);
        Account Get(string username);
        void Logout();
        IList<Account> SearchAccount(int idAccount);
        bool UpdateStatus(string username, string reason, bool status);
        bool CreateAccount(string username, byte typeOfUser);
        void writeLogUser(int idUser, bool status);
    }
}
