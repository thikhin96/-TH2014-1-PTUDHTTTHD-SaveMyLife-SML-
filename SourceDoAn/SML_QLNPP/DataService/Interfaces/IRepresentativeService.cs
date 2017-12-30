using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IRepresentativeService
    {
        bool CreateRepresentative(Representative person);
        bool UpdateRepresentative(int idDis);
        bool CheckPhone(string phone);
        bool CheckEmail(string email);
        int GenerateOrderId();
    }
}
