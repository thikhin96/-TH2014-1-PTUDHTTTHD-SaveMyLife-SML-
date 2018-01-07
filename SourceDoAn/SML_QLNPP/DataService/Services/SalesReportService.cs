using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using NLog;
using DataModel.Interfaces;

namespace DataService
{

    public class SalesReportService : ISalesReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public SalesReportService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SalesReport GetSalesReport(int id)
        {
            logger.Info("Start get SalesReport");
            IRepository<SalesReport> repository = _unitOfWork.Repository<SalesReport>();
            SalesReport result;
            try
            {
                logger.Info("Completed get promotion");
                result = repository.Get(a => a.idSalesReport == id);
            }
            catch (Exception ex)
            {
                logger.Info("Error get promotion: " + ex.Message);
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Tìm kiếm khuyến mãi theo id, ngày bắt đầu, ngày kết thúc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*public IList<SalesReport> Search(int idSalesReport, string beginDate, string endDate)
        {
            try
            {
                logger.Info("Start search promotion");
                IRepository<SalesReport> repository = _unitOfWork.Repository<SalesReport>();
                IList<SalesReport> result;
                if (idSalesReport == 0 && beginDate == "" && endDate == "") // Kiểm tra nếu không có dữ liệu lấy về toàn bộ danh sách khuyến mãi
                {
                    logger.Info("Start search ALL promotion");
                    result = repository.GetAll().ToList();
                }
                if (idSalesReport > 0) //nếu có ID thì lấy theo ID
                {
                    logger.Info("Start search BY ID promotion");
                    result = repository.GetAll(a => a.idSalesReport == idSalesReport).ToList();

                }
                else
                {

                    if (beginDate != "" && endDate != "") //kiểm tra ngày bắt đầu và ngày kết thúc
                    {
                        logger.Info("Start search START DATE & ENDDATE promotion");
                        var sdate = Convert.ToDateTime(beginDate);
                        var edate = Convert.ToDateTime(endDate);
                        result = repository.GetAll(a => (a.beginDate.Value.Year == sdate.Year
                                            && a.beginDate.Value.Month == sdate.Month
                                            && a.beginDate.Value.Day == sdate.Day)
                                            &&
                                            (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                    else if (beginDate != "") //nếu ngày bắt đầu khác null
                    {
                        logger.Info("Start search START DATE promotion");
                        var sdate = Convert.ToDateTime(beginDate);
                        result = repository.GetAll(a => (a.beginDate.Value.Year == sdate.Year
                                            && a.beginDate.Value.Month == sdate.Month
                                            && a.beginDate.Value.Day == sdate.Day)).ToList();
                    }
                    else//nếu ngày kết thúc khác null
                    {
                        logger.Info("Start search ENDDATE promotion");
                        var edate = Convert.ToDateTime(endDate);
                        result = repository.GetAll(a => (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                }
                logger.Info("Completed search promotion");
                return result;

            }
            catch (Exception ex)
            {
                logger.Info("Error search promotion: " + ex.Message);
                return null;
            }
        }*/
        public IList<Bill> SearchListBill(int idBill, int idDistributor, string CreateDate)
        {
            try
            {
                logger.Info("Start search Bill");
                IRepository<Bill> repository = _unitOfWork.Repository<Bill>();
                IList<Bill> result;
                var cDate = Convert.ToDateTime(CreateDate);
                if (idDistributor == 0 && CreateDate == "") // Kiểm tra nếu không có dữ liệu lấy về toàn bộ danh sách Order
                {
                    logger.Info("Start search ALL Bill");
                    result = repository.GetAll().ToList();
                }
                if (idDistributor > 0) //nếu có ID thì lấy theo ID
                {
                    logger.Info("Start search BY ID distributor");
                    result = repository.GetAll(a => a.idDistributor == idDistributor).ToList();

                }
                else
                {
                    if(CreateDate==null)
                    {

                    }

                }
                if (CreateDate != "") //kiểm tra ngày lập
                    {
                        logger.Info("Start search CreateDate Order");
                        var sdate = Convert.ToDateTime(CreateDate);
                        result = repository.GetAll(a => (a.createdDate.Value.Year == sdate.Year
                                            && a.createdDate.Value.Month == sdate.Month)).ToList();
                    }
                logger.Info("Completed search Order");
                return null;

            }
            catch (Exception ex)
            {
                logger.Info("Error search Order: " + ex.Message);
                return null;
            }
        }
        public IList<Order> SearchListOrder(int idOrder,int idDistributor, string CreateDate)
        {
            try
            {
                logger.Info("Start search Order");
                IRepository<Order> repository = _unitOfWork.Repository<Order>();
                IList<Order> result;
                int Month = Convert.ToDateTime(CreateDate).Month;
                int Year = Convert.ToDateTime(CreateDate).Year;
                if (idDistributor == 0 && CreateDate == "") // Kiểm tra nếu không có dữ liệu lấy về toàn bộ danh sách Order
                {
                    logger.Info("Start search ALL Order");
                    result = repository.GetAll().ToList();
                }
                if (idDistributor > 0) //nếu có ID thì lấy theo ID
                {
                    logger.Info("Start search BY ID Order");
                    result = repository.GetAll(a => a.idDistributor == idDistributor).ToList();

                }
                else
                {
                    if(Month!=0 &&Year!=0 && idDistributor!=0 )
                    {

                    }

                }
                if (CreateDate != "") //kiểm tra ngày lập
                    {
                        logger.Info("Start search CreateDate Order");
                        var sdate = Convert.ToDateTime(CreateDate);
                        result = repository.GetAll(a => (a.CreatedDate.Value.Year == sdate.Year
                                            && a.CreatedDate.Value.Month == sdate.Month)).ToList();
                    }
                logger.Info("Completed search Order");
                return null;

            }
            catch (Exception ex)
            {
                logger.Info("Error search Order: " + ex.Message);
                return null;
            }
        }

        IList<SalesReport> ISalesReportService.Search(int idSalesReport, string createDate, string endDate)
        {
            throw new NotImplementedException();
        }


        IList<SalesReport> ISalesReportService.SearchListOrder(int idOrder, int idDistributor, string CreateDate)
        {
            throw new NotImplementedException();
        }

        IList<SalesReport> ISalesReportService.SearchListBill(int idBil, int idDistributor, string CreateDate)
        {
            throw new NotImplementedException();
        }
    }
}
