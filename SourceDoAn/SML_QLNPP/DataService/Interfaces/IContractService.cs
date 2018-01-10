using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Dtos;

namespace DataService.Interfaces
{
    public interface IContractService
    {
        bool CreateContract(Contract con, bool type);
        bool CancelContract(int id, string reason);
        IList<ContractList> Search(int? keyword = null, int? criterion = null);
        Contract Get(int id);
    }
}
