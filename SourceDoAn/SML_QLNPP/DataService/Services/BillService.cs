using System.Collections.Generic;
using System.Linq;
using DataModel;
using NLog;
using DataModel.Interfaces;
namespace DataService.Interfaces
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AddBill(Bill bill)
        {
            IRepository<Bill> repository = _unitOfWork.Repository<Bill>();
            try {
                repository.Add(bill);
                _unitOfWork.SaveChange();
                return true;
            } catch
            {
                return false;
            }
            
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
