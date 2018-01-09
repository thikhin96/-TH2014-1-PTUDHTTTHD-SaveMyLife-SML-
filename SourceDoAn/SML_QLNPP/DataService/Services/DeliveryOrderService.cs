using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using NLog;
using DataService.Interfaces;
using DataModel.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace DataService.Services
{
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public DeliveryOrderService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public bool AddDeliveryOrder(DeliveryOrder dOrder)
        {
            //throw new NotImplementedException();
            logger.Info("Start Add DeliveryOrder....");
            bool rs = false;
            try
            {
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                repo.Add(dOrder);
                _unitOfWork.SaveChange();
                rs = true;
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Debug.WriteLine(@"Entity of type ""{0}"" in state ""{1}"" 
            //       has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name,
            //            eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Debug.WriteLine(@"- Property: ""{0}"", Error: ""{1}""",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            //catch (DbUpdateException e)
            //{
            //    throw;
            //}
            catch (Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End Add DeliveryOrder....");
            return rs;
        }
        public bool UpdateDeliveryOrder(DeliveryOrder dOrder)
        {
            logger.Info("Start Update DeliveryOrder....");
            bool rs = false;
            try
            {
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                repo.Update(dOrder);
                _unitOfWork.SaveChange();
                rs = true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
            }
            logger.Info("End Update DeliveryOrder....");
            return rs;
        }

        public DeliveryOrder SearchById(int id)
        {
            logger.Info("Start Search DeliveryOrder by id DeliveryOrder....");
            DeliveryOrder dOrder = null;
            try
            {
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                dOrder = repo.Get(a => a.idDeliveryOrder == id);
            }
            catch(Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End Search DeliveryOrder by id DeliveryOrder....");
            return dOrder;
        }


        public IList<DeliveryOrder> SearchByStatus(byte status)
        {
            logger.Info("Start Search DeliveryOrder by Status....");
            IList<DeliveryOrder> rs = null;
            try
            {
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                rs= repo.GetAll(a => a.status == status).ToList();
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End Search DeliveryOrder by Status....");
            return rs;
        }
        public IList<DeliveryOrder> SearchByDeliveryDate(DateTime datetime)
        {
            logger.Info("Start Search DeliveryOrder by Status....");
            IList<DeliveryOrder> rs = null;
            try
            {
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                rs = repo.GetAll(a => (a.deliveryDate.Value.Year == datetime.Year
                    && a.deliveryDate.Value.Month == datetime.Month
                    && a.deliveryDate.Value.Day == datetime.Day)).ToList();
            }
            catch(Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End Search DeliveryOrder by DeliveryDate....");
            return rs;
        }
        // can them vao
        public IList<DeliveryOrder> GetAll()
        {
            logger.Info("Start Search DeliveryOrder by Status....");
            IList<DeliveryOrder> rs = null;
            try
            {
                logger.Info("Start GetAll DeliveryOrder....");
                IRepository<DeliveryOrder> repo = _unitOfWork.Repository<DeliveryOrder>();
                rs = repo.GetAll().ToList();
            }
            catch(Exception ex)
            {
                logger.Info(ex);
            }
            logger.Info("End GetAll DeliveryOrder....");
            return rs;
        }
    }
}
