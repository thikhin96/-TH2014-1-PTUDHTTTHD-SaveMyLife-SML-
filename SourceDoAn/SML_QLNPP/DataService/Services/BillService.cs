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
            logger.Info("Start Add Bill....");
            bool rs = false;
            try {

                IRepository<Bill> repo = _unitOfWork.Repository<Bill>();
                repo.Add(bill);
                _unitOfWork.SaveChange();
                rs = true;
            } catch(Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End Add Bill....");
            return rs;
        }
    }
}
