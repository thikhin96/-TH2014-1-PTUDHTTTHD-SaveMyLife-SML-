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
        public PromotionService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public Promotion GetPromotion(int id)
        {
            IRepository<Promotion> repository = _unitOfWork.Repository<Promotion>();
            var result = repository.Get(a => a.idPromotion == id);
            return result != null ? result : null;
        }

        public IList<Promotion> Search(int idpro, string createDate,string endDate)
        {
            try
            {
                IRepository<Promotion> repository = _unitOfWork.Repository<Promotion>();
                if(idpro == 0 && createDate=="" && endDate == "")
                {
                    return repository.GetAll().ToList();
                }
                if (idpro > 0)
                {
                   
                    return repository.GetAll(a => a.idPromotion == idpro).ToList();
                    
                }
                else
                {
                    var sdate = Convert.ToDateTime(createDate);
                    var edate = Convert.ToDateTime(endDate);
                    if (createDate != "" && endDate != "")
                    {
                        return repository.GetAll(a => (a.startDate.Value.Year == sdate.Year
                                            && a.startDate.Value.Month == sdate.Month
                                            && a.startDate.Value.Day == sdate.Day) 
                                            && 
                                            (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                    else if(createDate != "")
                    {
                        return repository.GetAll(a => (a.startDate.Value.Year == sdate.Year
                                            && a.startDate.Value.Month == sdate.Month
                                            && a.startDate.Value.Day == sdate.Day)).ToList();
                    }
                    else
                    {
                        return repository.GetAll(a => (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                }

            }
            catch
            {
                return null;
            }
        }
    }
}
