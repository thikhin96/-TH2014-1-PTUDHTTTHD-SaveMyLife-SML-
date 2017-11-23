using DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    class PromotionService : IPromotionService
    {
        //private readonly IRepository<Promotion> _promotionRepository;
        //private readonly IUnitOfWork _unitOfWork;

        //public Promotion(IRepository<Promotion> promotionRepository, IUnitOfWork unitOfWork)
        //{
        //    _promotionRepository = promotionRepository;
        //    _unitOfWork = unitOfWork;
        //}

        int IPromotionService.DeletePromotion(int id)
        {
            throw new NotImplementedException();
        }
    }
}
