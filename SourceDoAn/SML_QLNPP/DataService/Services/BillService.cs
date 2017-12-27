using System.Collections.Generic;
using System.Linq;
using DataModel;
using NLog;
using DataModel.Interfaces;
using System;

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
            try {
                logger.Info("Start Add Bill....");
                IRepository<Bill> repository = _unitOfWork.Repository<Bill>();
                repository.Add(bill);
                _unitOfWork.SaveChange();
                logger.Info("End Add Bill....");
                return true;
            } catch(Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Add Bill....");
                return false;
            } 
        }
        public IList<Bill> SearchById(int id)
        { 
            try
            {
                logger.Info("Start Search Bill by idBill....");
                IRepository<Bill> repository = _unitOfWork.Repository<Bill>();
                var result = repository.GetAll(a => a.idBill == id);
                if (result != null)
                {
                    logger.Info("End Search Bill by idBill....");
                    return result.ToList();
                }
                else
                {
                    logger.Info("End Search Bill by idBill....");
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Search Bill by idBill....");
                return null;
            }  
        }
    }
}
