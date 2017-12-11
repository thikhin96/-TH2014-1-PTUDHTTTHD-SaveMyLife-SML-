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
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Distributor> _distributorRepository;
        private readonly IRepository<Consignee> _consigneeRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IDistributorService _distributorService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public OrderService(IUnitOfWork unitOfWork, IDistributorService distributorService)
        {
            _orderRepository = unitOfWork.Repository<Order>();
            _distributorRepository = unitOfWork.Repository<Distributor>();
            _consigneeRepository = unitOfWork.Repository<Consignee>();
            _orderDetailRepository = unitOfWork.Repository<OrderDetail>();
            _unitOfWork = unitOfWork;
            _distributorService = distributorService;
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

        public int GenerateOrderId()
        {
            var latestOrder = _orderRepository.GetAll().OrderByDescending(x => x.idOrder).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idOrder + 1;
            else
                return 0;
        }

        public string CreateOrder(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                if (_distributorRepository.Get(x => x.idDistributor == order.idDistributor) != null)
                {
                    if (_distributorService.hasContract(order.idDistributor ?? 0))
                    {
                            _orderRepository.Add(order);
                            _unitOfWork.SaveChange();
                            var consignee = order.Consignee;
                            _consigneeRepository.Update(consignee);
                            if (orderDetails.Count != 0 || orderDetails != null)
                                orderDetails.ForEach(x => _orderDetailRepository.Add(x));
                            _unitOfWork.SaveChange();
                    }
                    else
                    {
                        return "Nhà phân phối hiện không có bất cứ hợp đồng nào";
                    }
                }
                else
                {
                    return "Nhà phân phối không tồn tại";
                }
                return "thanh cong";
            }
            catch
            {
                return "Không thể tạo đơn đặt hàng";
                throw;
            }


        }
    }
}
