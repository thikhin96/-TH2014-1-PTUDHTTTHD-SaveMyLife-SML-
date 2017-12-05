using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace DataService.Interfaces
{
    public interface IOrderService
    {
        bool CheckDept(int idDistributor);
        IList<DonDatHang> SearchOrder(string keyword, string createDate, int status);
        int AddOrder(DonDatHang order);
        int UpdateOrder(DonDatHang order);
        DonDatHang GetOrder(int id);
    }
}
