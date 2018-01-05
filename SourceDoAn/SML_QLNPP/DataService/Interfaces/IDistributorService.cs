using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Dtos;

namespace DataService.Interfaces
{
    public interface IDistributorService: IDistributorBaseService
    {
        bool Create(PotentialDistributor pDis);
        bool UpdateDebt(int id, long money);
        bool UpdateStatus(int id, bool status);
        Distributor SearchByID(int id);
        IList<DistributorList> GetList(Nullable<int> id);
        IList<DistributorList> GetAll();
        bool hasContract(int distributorId);
        bool priceOverDebt(int distributorId, decimal price);
        Distributor GetDistributor(int idd);
        IEnumerable GetList();
    }
}
