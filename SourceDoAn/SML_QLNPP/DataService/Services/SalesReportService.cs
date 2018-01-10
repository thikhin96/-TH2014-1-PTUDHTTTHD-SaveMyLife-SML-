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
        
    }
}
