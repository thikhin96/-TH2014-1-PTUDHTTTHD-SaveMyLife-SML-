using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Interfaces;

namespace SML_QLNPP.Controllers
{
    public class PromotionController
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
    }
}