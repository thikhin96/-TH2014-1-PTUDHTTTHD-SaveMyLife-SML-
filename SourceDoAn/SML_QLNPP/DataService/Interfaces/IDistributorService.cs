using System;
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
        IList<DistributorList> GetAll();
    }
}
