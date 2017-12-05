using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace DataService.Interfaces
{
    public interface IPromotionService
    {
        IList<Promotion> Search(int idpro, string createDate,string endDate);
        Promotion GetPromotion(int id);
    }
}
