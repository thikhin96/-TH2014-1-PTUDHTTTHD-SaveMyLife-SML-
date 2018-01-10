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
            try
            {
                logger.Info("Start Add DeliveryOrder....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                repository.Add(dOrder);
                _unitOfWork.SaveChange();
                logger.Info("End Add DeliveryOrder....");
                return true;
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
                logger.Info("End Add DeliveryOrder....");
            }
            return false;
        }
        public bool UpdateDeliveryOrder(DeliveryOrder dOrder)
        {
            try
            {
                logger.Info("Start Update DeliveryOrder....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                repository.Update(dOrder);
                _unitOfWork.SaveChange();
                logger.Info("End Update DeliveryOrder....");
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Update DeliveryOrder....");
                return false;
            }
        }

        public DeliveryOrder SearchById(int id)
        {
            try
            {
                logger.Info("Start Search DeliveryOrder by id DeliveryOrder....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                var result = repository.Get(a => a.idDeliveryOrder == id);
                if (result!= null)
                {
                    logger.Info("End Search DeliveryOrder by id DeliveryOrder....");
                    return result;

                }
                else
                {
                    logger.Info("End Search DeliveryOrder by id DeliveryOrder....");
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Search DeliveryOrder by id DeliveryOrder....");
                return null;
            }
        }


        public IList<DeliveryOrder> SearchByStatus(byte status)
        {
            try
            {
                logger.Info("Start Search DeliveryOrder by Status....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                var result = repository.GetAll(a => a.status == status);
                if (result != null)
                {
                    logger.Info("End Search DeliveryOrder by Status....");
                    return result.ToList();
                }
                else
                {
                    logger.Info("End Search DeliveryOrder by Status....");
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Search DeliveryOrder by Status....");
                return null;
            }
        }
        public IList<DeliveryOrder> SearchByDeliveryDate(DateTime datetime)
        {
            try
            {
                logger.Info("Start Search DeliveryOrder by DeliveryDate....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                var dtime = Convert.ToDateTime(datetime);
                var result = repository.GetAll(a => (a.deliveryDate.Value.Year == dtime.Year
                    && a.deliveryDate.Value.Month == dtime.Month
                    && a.deliveryDate.Value.Day == dtime.Day));
                if (result != null)
                {
                    logger.Info("End Search DeliveryOrder by DeliveryDate....");
                    return result.ToList();
                }
                else
                {
                    logger.Info("End Search DeliveryOrder by DeliveryDate....");
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End Search DeliveryOrder by DeliveryDate....");
                return null;
            }
        }
        // can them vao
        public IList<DeliveryOrder> GetAll()
        {
            try
            {
                logger.Info("Start GetAll DeliveryOrder....");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                var result = repository.GetAll();
                if (result != null)
                {
                    logger.Info("Start GetAll DeliveryOrder....");
                    return result.ToList();
                }
                else
                {
                    logger.Info("Start GetAll DeliveryOrder....");
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                logger.Info("End GetAll DeliveryOrder....");
                return null;
            }
        }
        public int GenerateDOrderId()
        {
            IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
            var latestdOrder = repository.GetAll().OrderByDescending(x => x.idDeliveryOrder).FirstOrDefault();
            if (latestdOrder != null)
                return latestdOrder.idDeliveryOrder + 1;
            else
                return 1;
        }
        public IList<DeliveryOrder> SearchListDeliveryOrder(int month, int quartar, int year, int idDistributor)
        {
            try
            {
                logger.Info("Start search order");
                IRepository<DeliveryOrder> repository = _unitOfWork.Repository<DeliveryOrder>();
                if (month == 0 && quartar == 0 && year == 0 && idDistributor == 0)
                {
                    logger.Info("Start search ALL order");
                    return repository.GetAll().ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                logger.Info("Error Search Order: " + ex.Message);
                return null;
            }
        }
    }
}
