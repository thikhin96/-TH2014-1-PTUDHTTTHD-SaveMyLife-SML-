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

        public int UpdateOrder(Order order)
        {
            throw new NotImplementedException();
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
        /// <summary>
        /// Tìm kiếm đơn đặt hàng Tháng/ Quý/ Năm
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="idDistributor"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrderInMonth(int month)
        {
            logger.Info("Start search order");
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            return repository.GetAll(m => m.CreatedDate.Value.Month == month);
        }
        public IEnumerable<Order> GetOrderInQuartar(int quartar)
        {
            logger.Info("Start search order");
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            return repository.GetAll(q => (q.CreatedDate.Value.Month >= (quartar - 1) * 3 + 1) && (q.CreatedDate.Value.Month <= quartar * 3));

        }
        public IEnumerable<Order> GetOrderInYear(int year)
        {
            logger.Info("Start search order");
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            return repository.GetAll(y => y.CreatedDate.Value.Year == year);
        }
        public IEnumerable<Order> GetOrderByDistributor(int? dtbtrId)
        {
            logger.Info("Start search order");
            IRepository<Order> repository = _unitOfWork.Repository<Order>();
            if(dtbtrId == null)
            {
                return repository.GetAll();
            }
            return repository.GetAll(dis => (dis.idDistributor ?? default(int)) == dtbtrId);
        }


        /// <summary>
        /// Tìm kiếm đơn đặt hàng Tháng/ Quý/ Năm
        /// </summary>
        /// <param name="idDistributor"></param>
        /// <param name="createDate"></param>
        /// <param name="month"></param>
        /// <param name="quartar"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IList<Order> SearchListOrder(int? idDistributor, string createDate, int month, int quartar, int year)
        {
            try
            {
                logger.Info("Start search order");
                IRepository<Order> repository = _unitOfWork.Repository<Order>();
                if(createDate == "" || createDate == null)
                {
                    if (idDistributor==0)
                    {
                        return null;
                    }
                    if( DateTime.Parse(createDate).Month != 0)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                logger.Info("Error Search Order: " + ex.Message);
                return null;
            }
        }

    }
}
