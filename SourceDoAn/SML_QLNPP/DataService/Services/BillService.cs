using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.Interfaces;
using NLog;
namespace DataService.Interfaces
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public BillService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public bool AddBill(Bill Bill)
        {
            throw new NotImplementedException();
        }
        public IList<Bill> SearchById(int id)
        {
            IRepository<Bill> repository = _unitOfWork.Repository<Bill>();
            var result = repository.GetAll(a => a.idBill == id);
            if (result != null){
                return result.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
