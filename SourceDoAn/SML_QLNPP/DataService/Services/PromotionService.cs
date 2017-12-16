using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using NLog;
namespace DataService
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public PromotionService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy thông tin của một khuyến mãi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Promotion GetPromotion(int id)
        {
            logger.Info("Start get promotion");
            IRepository<Promotion> repository = _unitOfWork.Repository<Promotion>();
            Promotion result;
            try
            {
                logger.Info("Completed get promotion");
                result = repository.Get(a => a.idPromotion == id);
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
        public IList<Promotion> Search(int idpro, string startDate,string endDate)
        {
            try
            {
                logger.Info("Start search promotion");
                IRepository<Promotion> repository = _unitOfWork.Repository<Promotion>();
                IList<Promotion> result;
                if (idpro == 0 && startDate=="" && endDate == "") // Kiểm tra nếu không có dữ liệu lấy về toàn bộ danh sách khuyến mãi
                {
                    logger.Info("Start search ALL promotion");
                    result = repository.GetAll().ToList();
                }
                if (idpro > 0) //nếu có ID thì lấy theo ID
                {
                    logger.Info("Start search BY ID promotion");
                    result = repository.GetAll(a => a.idPromotion == idpro).ToList();
                    
                }
                else
                {
                    
                    if (startDate != "" && endDate != "") //kiểm tra ngày bắt đầu và ngày kết thúc
                    {
                        logger.Info("Start search START DATE & ENDDATE promotion");
                        var sdate = Convert.ToDateTime(startDate);
                        var edate = Convert.ToDateTime(endDate);
                        result = repository.GetAll(a => (a.startDate.Value.Year == sdate.Year
                                            && a.startDate.Value.Month == sdate.Month
                                            && a.startDate.Value.Day == sdate.Day) 
                                            && 
                                            (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                    else if(startDate != "") //nếu ngày bắt đầu khác null
                    {
                        logger.Info("Start search START DATE promotion");
                        var sdate = Convert.ToDateTime(startDate);
                        result = repository.GetAll(a => (a.startDate.Value.Year == sdate.Year
                                            && a.startDate.Value.Month == sdate.Month
                                            && a.startDate.Value.Day == sdate.Day)).ToList();
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
            catch(Exception ex)
            {
                logger.Info("Error search promotion: " + ex.Message);
                return null;
            }
        }
    }
}
