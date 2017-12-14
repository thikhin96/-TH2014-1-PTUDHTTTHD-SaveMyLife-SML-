using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using NLog;
using DataService.Interfaces;
using DataModel.Interfaces;

namespace DataService.Services
{
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public DeliveryOrderService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public bool AddDeliveryOrder(DeliveryOrder dOrder)
        {
            throw new NotImplementedException();
        }
        public bool UpdateDeliveryOrder(DeliveryOrder dOrder)
        {
            try
            {
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                repository.Update(dOrder);
                _unitOfWork.SaveChange();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DeliveryOrder SearchById(int id)
        {
            try
            {
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                var result = repository.Get(a => a.idDeliveryOrder == id);
                return result != null ? result : null;
            }
            catch
            {
                return null;
            }
        }


        public IList<DeliveryOrder> SearchByStatus(byte status)
        {
            IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
            var result = repository.GetAll(a => a.status == status);
            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }
        public IList<DeliveryOrder> SearchByDeliveryDate(DateTime datetime)
        {
            IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
            var dtime = Convert.ToDateTime(datetime);
            var result = repository.GetAll(a => (a.deliveryDate.Value.Year == dtime.Year
                && a.deliveryDate.Value.Month == dtime.Month
                && a.deliveryDate.Value.Day == dtime.Day));
            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }
        // can them vao
        public IList<DeliveryOrder> GetAll()
        {
            IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
            var result = repository.GetAll();
            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
