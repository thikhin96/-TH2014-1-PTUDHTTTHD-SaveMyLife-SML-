using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class PromotionService : IPromotionService
    {
        //private readonly IRepository<Promotion> _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PromotionService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }

        public string InjectAlert()
        {
            return "Hey you have successfully injected dependencies";
        }

        public int DeletePromotion(int id)
        {
            //bla bla bla
            return 0;
        }
    }
}
