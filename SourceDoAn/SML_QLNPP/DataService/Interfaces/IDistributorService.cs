using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Dtos;

namespace DataService.Interfaces
{
    public interface IDistributorService
    {
        int Create(Distributor person);
        int GenerateDistributorId();
        bool UpdateDebt(int id, long money);
        bool UpdateStatus(int id, bool status, string note);
        Distributor SearchByID(int id);
        IList<DistributorList> GetList(Nullable<int> id, bool? status);
        bool hasContract(int distributorId);
        List<Distributor> SearchByName(string searchTerm);
        Contract GetCurrentContract(int distributorId);
        bool exceedingDebt(int distributorId);
        List<Storage> GetStorages(string keyWord, int distributorId);
        bool UpdateDebt(int id, decimal money);
    }
}
