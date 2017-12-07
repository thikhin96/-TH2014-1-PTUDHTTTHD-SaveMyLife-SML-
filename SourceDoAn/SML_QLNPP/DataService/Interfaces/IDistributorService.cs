using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface IDistributorService
    {
        bool hasContract(int distributorId);
        bool priceOverDebt(int distributorId, decimal price);
    }
}
