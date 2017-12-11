using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IDistributorBaseService
    {
        bool CheckPhone(string phone);
        bool CheckEmail(string email);
        DistributorBase SearchByID(int id);
        bool Create(DistributorBase dis, Representative rep);
    }
}
