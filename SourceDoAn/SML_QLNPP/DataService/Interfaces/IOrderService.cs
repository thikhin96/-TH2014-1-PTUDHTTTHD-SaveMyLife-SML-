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
        string UpdateOrder(Order order);
        Order GetOrder(int id);
        int GenerateOrderId();
        string CreateOrder(Order order, List<OrderDetail> orderDetails);
        IList<Order> SearchListOrder(int? idDistributor, string CreateDate, int month, int quartar, int year);
        IEnumerable<Order> GetOrderByDistributor(int? dtbtrId);
        dynamic GetOrderByDistributor();
        string CreateOrder(Order order);
    }
}
