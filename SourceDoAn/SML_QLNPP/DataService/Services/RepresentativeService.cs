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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Representative> _representativeRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public RepresentativeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _representativeRepository = unitOfWork.Repository<Representative>();
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

        public bool UpdateRepresentative(int idDis)
        {
            throw new NotImplementedException();
        }

        public int GenerateOrderId()
        {
            var latestOrder = _representativeRepository.GetAll().OrderByDescending(x => x.idRepresentative).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idRepresentative + 1;
            else
                return 0;
        }
    }
}
