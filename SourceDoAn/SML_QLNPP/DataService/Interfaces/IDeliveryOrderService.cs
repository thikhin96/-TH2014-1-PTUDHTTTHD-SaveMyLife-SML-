using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace DataService.Interfaces
{
    public interface IDeliveryOrderService
    {
        bool AddDeliveryOrder(DeliveryOrder dOrder);
        bool UpdateDeliveryOrder(DeliveryOrder dOrder);
        DeliveryOrder SearchById(int id);
        IList<DeliveryOrder> SearchByStatus(byte status);
        IList<DeliveryOrder> SearchByDeliveryDate(DateTime datetime);

        // can them vao
        IList<DeliveryOrder> GetAll();
    }
}
