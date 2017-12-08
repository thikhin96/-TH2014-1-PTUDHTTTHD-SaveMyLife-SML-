using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.Interfaces;
using NLog;
namespace DataService.Interfaces
{
    public class DetailedDeliveryOrderService : IDetailedDeliveryOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public DetailedDeliveryOrderService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public bool AddDetailedDeliveryOrder(DetailedDeliveryOrder ddOrder)
        {
            throw new NotImplementedException();
        }

        public IList<DetailedDeliveryOrder> SearchByIdDeliveryOrder(int idDeliveryOrder)
        {
            IRepository<DetailedDeliveryOrder> repository = _unitOfWork.Repository<DetailedDeliveryOrder>();
            var result = repository.GetAll(a => a.idDeliveryOrder == idDeliveryOrder);
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
