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

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tìm kiếm đơn đặt hàng theo từ khoá, ngày tạo hoặc trạng thái
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="createDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<Order> SearchOrder(string keyword, string createDate, int status)
        {
            try
            {
                logger.Info("Start search order");
                IRepository<Order> repository = _unitOfWork.Repository<Order>();
                if(createDate=="" || createDate == null)
                {
                    if (keyword != null && keyword != "")
                    {
                        return repository.GetAll(a => (a.Staff.staffName.Contains(keyword) || a.Distributor.name.Contains(keyword) || a.Consignee.Name.Contains(keyword)) && a.Statuses == status).ToList();
                    }
                    else
                    {
                        logger.Info("Completed search order");
                        return repository.GetAll(a => a.Statuses == status).ToList();
                    }
                }
                else
                {
                    var date = Convert.ToDateTime(createDate);
                    if (keyword != null && keyword != "")
                    {
                        logger.Info("Completed search order");
                        return repository.GetAll(a => (a.Staff.staffName.Contains(keyword) || a.Distributor.name.Contains(keyword) || a.Consignee.Name.Contains(keyword)) && (a.CreatedDate.Value.Year == date.Year
                                            && a.CreatedDate.Value.Month == date.Month
                                            && a.CreatedDate.Value.Day == date.Day) && a.Statuses == status).ToList();
                    }
                    else
                    {
                        logger.Info("Completed search order");
                        return repository.GetAll(a => (a.CreatedDate.Value.Year == date.Year
                                            && a.CreatedDate.Value.Month == date.Month
                                            && a.CreatedDate.Value.Day == date.Day) && a.Statuses == status).ToList();
                    }
                }
                
            }
            catch(Exception ex)
            {

                logger.Info("Error Search Order: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Lấy thông tin đơn đặt hàng từ 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="createDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            logger.Info("Start get order");
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            Order result;
            try
            {
                logger.Info("Completed get order");
                result = repository.Get(a => a.idOrder == id);
            }
            catch(Exception ex)
            {
                logger.Info("Error get order: " + ex.Message);
                result = null;
            }
            return result;
        }

        public int GenerateOrderId()
        {
            var latestOrder = _orderRepository.GetAll().OrderByDescending(x => x.idOrder).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idOrder + 1;
            else
                return 0;
        }

        public string CreateOrder(Order order)
        {
            try
            {
                var announcement = "thanh cong";
                var distributor = _distributorRepository.Get(x => x.idDistributor == order.idDistributor);
                if (distributor != null)
                {
                    var contract = _distributorService.GetCurrentContract(order.idDistributor ?? 0);
                    if (contract != null)
                    {
                        if (contract.maxDebt > distributor.debt.GetValueOrDefault())
                        {
                            if (contract.minOrderTotalValue.GetValueOrDefault() <= order.Total.GetValueOrDefault())
                            {
                                _orderRepository.Add(order);
                                _unitOfWork.SaveChange();
                            }
                            else
                                announcement = "Tổng tiền của đơn đặt hàng thấp hơn mức quy định";
                        }
                        else
                            announcement = "Nhà phân phối đã nợ quá số tiền cho phép";
                    }
                    else
                        announcement = "Nhà phân phối hiện không có bất cứ hợp đồng nào";
                }
                else
                   announcement = "Nhà phân phối không tồn tại";
                return announcement;
            }
            catch (Exception ex)
            {
                return "Không thể tạo đơn đặt hàng";
                throw;
            }


        }

        public string UpdateOrder(Order order)
        {
            try
            {
                _orderRepository.Update(order);
                var consignee = order.Consignee;
                _consigneeRepository.Update(consignee);
                var orderDetails = order.OrderDetails?.ToList();
                var thisOrderDetails = _orderDetailRepository.GetAll(x => x.idOrder == order.idOrder).ToList();
                thisOrderDetails.ForEach(x => _orderDetailRepository.Delete(x));
                _unitOfWork.SaveChange();
                if (orderDetails != null)
                {
                    orderDetails.ForEach(x => _orderDetailRepository.Add(x));
                    _unitOfWork.SaveChange();
                }
                return "thanh cong";
            }
            catch (Exception ex)
            {
                return "Không thể cập nhật đơn đặt hàng";
            }
        }
    }
}
