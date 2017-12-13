using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IContractService
    {
        bool CreateContract(Contract con, bool type);
        bool CheckLifeOfContract(short period);
        bool CheckDebtExcess(long money);
        bool CheckOrderTotalValue(long money);
        bool CancelContract(int id, string reason);
        IList<Contract> SearchContractCloseToExpiry(short period);
        IList<Contract> SearchByDistributor(int id);
        Contract SearchByID(int id);
    }
}
