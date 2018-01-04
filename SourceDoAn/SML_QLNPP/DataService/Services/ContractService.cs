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
        IUnitOfWork uow; 
        IRepository<Contract> repo_con;
        IRepository<Distributor> repo_dis;
        IRepository<PotentialDistributor> repo_pDis;
        IRepository<Representative> repo_rep;
        IRepresentativeService service_rep;
        IDistributorService service_dis;

        public ContractService(IUnitOfWork _unitOfWork, IRepresentativeService _serviceRep, IDistributorService _serviceDis)
        {
            uow = _unitOfWork;
            repo_con = uow.Repository<Contract>();
            repo_dis = uow.Repository<Distributor>();
            repo_pDis = uow.Repository<PotentialDistributor>();
            repo_rep = uow.Repository<Representative>();
            service_rep = _serviceRep;
            service_dis = _serviceDis;
        }

        public bool CancelContract(int id, string reason)
        {
            logger.Info("Start Service to cancel a contract...");
            
            Contract con = new Contract();
            //Distributor dis = new Distributor();

            con = Get(id);
            con.status = false;
            con.note = reason;
            Distributor dis = con.Distributor1;
            dis.status = false;
            
            bool result = true;
            try
            {
                repo_con.Update(con);
                repo_dis.Update(dis);
                uow.SaveChange();
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                result = false;
            }
            return result;
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

        int GenerateContractId()
        {
            var latestContract = repo_con.GetAll().OrderByDescending(x => x.idContract).FirstOrDefault();
            if (latestContract != null)
                return latestContract.idContract + 1;
            else
                return 1;
        }

        int GetCurrentContractOfDistributor(int? idDis)
        {
            logger.Info("Start Service to get id of current contract of distributor...");
            repo_con = uow.Repository<Contract>();
            Contract con = repo_con.Get(x => x.distributor == idDis && x.status == true);
            if (con != null)
            {
                return con.idContract;
            }
            return 0;
        }

        bool CreateContract_PDis(Contract contract)
        {
            try
            {
                Distributor dis = contract.Distributor1;
                dis.createdDate = DateTime.Now;
                dis.updatedDate = DateTime.Now;
                dis.debt = 0;
                dis.status = true; ;
                contract.distributor = service_dis.Create(dis);

                Representative rep = contract.Representative1;
                rep.PDistributor = null;
                rep.Distributor = contract.distributor;

                contract.representative = contract.Representative1.idRepresentative;

                repo_rep.Update(rep);
                repo_con.Add(contract);
                uow.SaveChange();
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
            return true;
        }

        bool CreateContract_Dis(Contract contract)
        {
            if (GetCurrentContractOfDistributor(contract.distributor) != 0)
                return false;
            try 
            {
                Distributor dis = contract.Distributor1;
                dis.status = true;
                dis.updatedDate = DateTime.Now;
                dis.note = "Tạo hợp đồng mới: " + contract.idContract.ToString();
                contract.Representative1.Distributor = contract.distributor;
                contract.representative = service_rep.Create(contract.Representative1);

                repo_dis.Update(dis);
                repo_con.Add(contract);
                uow.SaveChange();
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
            return true;
        }

        public bool CreateContract(Contract con, bool type)
        {
            try
            {
                con.idContract = GenerateContractId();
                con.status = true;
                bool result;
                if (type == false)
                {
                    result = CreateContract_Dis(con);
                }
                else
                {
                    result = CreateContract_PDis(con);
                }
                return result;
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                return false;
            }
        }

        public IList<ContractList> Search(int? keyword = null, int? criterion = null)
        {
            logger.Info("Start Service to search info of the distributor...");
            repo_con = uow.Repository<Contract>();
            IList<Contract> con = new List<Contract>();
            if (keyword == null)
            {
                con = repo_con.GetAll().ToList();
            }
            else 
            if (criterion == 1)       // search by id of distributor
            {
                con = repo_con.GetAll(x => x.distributor == keyword).OrderByDescending(x => x.beginDate).ToList();
            }
            else if (criterion == 2)  // search by id of contract
            {
                con = repo_con.GetAll(x => x.idContract == keyword).ToList();
            }
            else if (criterion == 3)  // Search contract that close expired date
            {
                DateTime date = DateTime.Now.AddDays((double)keyword);
                con = repo_con.GetAll(x => x.expiredDate <= date).OrderByDescending(x => x.expiredDate).ToList();
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
            logger.Info("Start Service to get detailed info of the contract...");
            Contract con = new Contract();
            con = repo_con.Get(x => x.idContract == id);
            return con;
        }
    }
}
