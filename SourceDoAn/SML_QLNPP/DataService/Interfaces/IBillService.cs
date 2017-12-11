using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace DataService.Interfaces
{
    public interface IBillService
    {
        bool AddBill(Bill bill);
        IList<Bill> SearchById(int id);
    }
}
