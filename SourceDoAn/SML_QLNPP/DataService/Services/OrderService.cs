using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Interfaces;
using DataService;
using NLog;
using DataModel.Interfaces;

namespace DataService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public OrderService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public int AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool CheckDept(int idDistributor)
        {
            throw new NotImplementedException();
        }

        public IList<Order> SearchOrder(string keyword, string createDate, int status)
        {
            try
            {
                IRepository<Order> repository = _unitOfWork.Repository<Order>();
                if(createDate=="" || createDate == null)
                {
                    if (keyword != null && keyword != "")
                    {
                        return repository.GetAll(a => (a.Staff.staffName.Contains(keyword) || a.Distributor.name.Contains(keyword) || a.Consignee.Name.Contains(keyword)) && a.Statuses == status).ToList();
                    }
                    else
                    {
                        return repository.GetAll(a => a.Statuses == status).ToList();
                    }
                }
                else
                {
                    var date = Convert.ToDateTime(createDate);
                    if (keyword != null && keyword != "")
                    {
                        return repository.GetAll(a => (a.Staff.staffName.Contains(keyword) || a.Distributor.name.Contains(keyword) || a.Consignee.Name.Contains(keyword)) && (a.CreatedDate.Value.Year == date.Year
                                            && a.CreatedDate.Value.Month == date.Month
                                            && a.CreatedDate.Value.Day == date.Day) && a.Statuses == status).ToList();
                    }
                    else
                    {
                        return repository.GetAll(a => (a.CreatedDate.Value.Year == date.Year
                                            && a.CreatedDate.Value.Month == date.Month
                                            && a.CreatedDate.Value.Day == date.Day) && a.Statuses == status).ToList();
                    }
                }
                
            }
            catch
            {
                return null;
            }
        }

        public int UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            var result = repository.Get(a => a.idOrder == id);
            return result != null ? result : null;
        }
    }
}
