using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.Repositories;
using DataModel.Interfaces;
using DataService.Interfaces;
using NLog;

namespace DataService.Services
{
    public class ContractService : IContractService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        public bool CancelContract(int id, string reason)
        {
            throw new NotImplementedException();
        }

        public bool CheckDebtExcess(long money)
        {
            throw new NotImplementedException();
        }

        public bool CheckLifeOfContract(short period)
        {
            throw new NotImplementedException();
        }

        public bool CheckOrderTotalValue(long money)
        {
            throw new NotImplementedException();
        }

        bool CreateContract_PDis(Contract contract)
        {
            return true;
        }

        bool CreateContract_Dis(Contract contract)
        {
            return true;
        }

        public bool CreateContract(Contract con, bool type)
        {
            throw new NotImplementedException();
        }

        public IList<Contract> SearchByDistributor(int id)
        {
            logger.Info("Start Service to get info of the distributor...");
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Contract> repo = uow.Repository<Contract>();
            IList<Contract> lCon = new List<Contract>();
            lCon = repo.GetAll(x => x.distributor == id).ToList();
            logger.Info("End service...");
            return lCon;
        }

        public Contract SearchByID(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Contract> SearchContractCloseToExpiry(short period)
        {
            throw new NotImplementedException();
        }
    }
}
