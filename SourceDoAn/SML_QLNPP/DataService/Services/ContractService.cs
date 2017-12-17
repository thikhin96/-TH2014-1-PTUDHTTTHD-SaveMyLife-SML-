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
using DataService.Dtos;

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

        public IList<ContractList> Search(int? keyword = null, int? criterion = null)
        {
            logger.Info("Start Service to search info of the distributor...");
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Contract> repo = uow.Repository<Contract>();
            IList<Contract> con = new List<Contract>();
            if (criterion == 1)       // search by id of distributor
            {
                con = repo.GetAll(x => x.distributor == keyword).OrderByDescending(x => x.beginDate).ToList();
            }
            else if (criterion == 2)  // search by id of contract
            {
                con = repo.GetAll(x => x.idContract == keyword).ToList();
            }
            else if (criterion == 3)  // Search contract that close expired date
            {
                DateTime date = DateTime.Now.AddDays((double)keyword);
                con = repo.GetAll(x => x.expiredDate <= date).OrderByDescending(x => x.expiredDate).ToList();
            }
            else
            {
                con = repo.GetAll().ToList();
            }

            //if (con.Count() == 0) return null;

            IList<ContractList> listCon = new List<ContractList>();
            ContractList lCon;
            foreach (var item in con)
            {
                lCon = new ContractList();
                lCon.BeginDate = item.beginDate;
                lCon.Dis_Name = item.Distributor1.name;
                lCon.ExpriedDate = item.expiredDate;
                lCon.IdCon = item.idContract;
                lCon.Status = item.status;
                listCon.Add(lCon);
            }
            logger.Info("End service...");
            return listCon;
        }

        public Contract Get(int id)
        {
            logger.Info("Start Service to get detailed info of the distributor...");
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Contract> repo = uow.Repository<Contract>();
            Contract con = new Contract();
            con = repo.Get(x => x.idContract == id);
            return con;
        }
    }
}
