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
        IList<Order> SearchOrder(string keyword, string createDate, int status);
        int AddOrder(Order order);
        int UpdateOrder(Order order);
        Order GetOrder(int id);
        int GenerateOrderId();
        string CreateOrder(Order order, List<OrderDetail> orderDetails);
    }
}
