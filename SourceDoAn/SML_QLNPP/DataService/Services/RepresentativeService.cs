using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Interfaces;
using DataModel;
using DataModel.Interfaces;
using NLog;

namespace DataService.Services
{
    public class RepresentativeService : IRepresentativeService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork uow;
        IRepository<Representative> repo_rep;

        public RepresentativeService(IUnitOfWork _unitOfWork)
        {
            uow = _unitOfWork;
            repo_rep = uow.Repository<Representative>();
        }

        int GenerateRepresentativeId()
        {
            var latestRep = repo_rep.GetAll().OrderByDescending(x => x.idRepresentative).FirstOrDefault();
            if (latestRep != null)
                return latestRep.idRepresentative + 1;
            else
                return 1;
        }

        public int Create(Representative person)
        {
            logger.Info("Start to create a representative...");
            person.idRepresentative = GenerateRepresentativeId();
            try
            {
                repo_rep.Add(person);
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
            return person.idRepresentative;
        }

    }
}
