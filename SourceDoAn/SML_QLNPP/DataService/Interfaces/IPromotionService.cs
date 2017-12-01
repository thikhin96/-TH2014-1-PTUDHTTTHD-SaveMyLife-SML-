using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface IPromotionService
    {
        string InjectAlert();
        //int AddPromotion(Promotion p);
        int DeletePromotion(int id);
        //int UpdatePromotion(Promotion p);
        //IList<Promotion> SearchPromotion();
    }
}
