using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataModel.Interfaces;
using DataService.Interfaces;
using DataModel;
using NLog;

namespace DataService.Services
{
    public class RepresentativeService : IRepresentativeService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Representative> _representativeRepository;
        
        public RepresentativeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _representativeRepository = _unitOfWork.Repository<Representative>();
        }

        public int Create(Representative person)
        {
            logger.Info("Start to create a representative...");
            person.idRepresentative = GenerateRepresentativeId();
            try
            {
                _representativeRepository.Add(person);
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
            return person.idRepresentative;
        }


        public bool CheckEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public bool CreateRepresentative(Representative person)
        {
            logger.Info("Start add representative method");
            try
            {
                _representativeRepository.Add(person);
                _unitOfWork.SaveChange();
                logger.Info("Status: Success");
            }
            catch
            {
                logger.Info("Status: Fail");
                return false;
            }
            return true;
        }

        public bool UpdateTypeOfRepresentation(int idRep,int idDis)
        {
            logger.Info("Start to update...");
            try
            {
                Representative rep = GetByID(idRep);
                rep.PDistributor = null;
                rep.Distributor = idDis;

                _representativeRepository.Update(rep);
                _unitOfWork.SaveChange();
                logger.Info("End: Successfull...");
                return true;
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                return false;
            }

        }

        public int GenerateRepresentativeId()
        {
            var latestOrder = _representativeRepository.GetAll().OrderByDescending(x => x.idRepresentative).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idRepresentative + 1;
            else
                return 0;
        }

        public Representative GetByID(int id)
        {
            logger.Info("Start to get id of a representative...");
            try
            {
                return _representativeRepository.Get(x => x.idRepresentative == id);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
        }
    }
}
