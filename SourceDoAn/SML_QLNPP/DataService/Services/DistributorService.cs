using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Interfaces;
using DataModel.Interfaces;
using DataModel.Repositories;
using DataService.Dtos;
using NLog;

namespace DataService.Services
{
    public class DistributorService : IDistributorService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        public bool CheckEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public bool Create(PotentialDistributor pDis)
        {
            throw new NotImplementedException();
        }

        public bool Create(DistributorBase dis, Representative rep)
        {
            throw new NotImplementedException();
        }

        public IList<DistributorList> GetList(Nullable<int> id = null)
        {
            logger.Info("Start service....");
            IList<Distributor> ds_Dis = new List<Distributor>();
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Distributor> repo = uow.Repository<Distributor>();
            if (id == null)
                ds_Dis = repo.GetAll().ToList();
            else
                ds_Dis = repo.GetAll(x => x.idDistributor == id).ToList();
            IList<DistributorList> listDis = new List<DistributorList>();
            DistributorList lDis;
            foreach (Distributor dis in ds_Dis)
            {
                lDis = new DistributorList();
                lDis.Dis = (DistributorBase)dis;
                foreach (Contract con in dis.Contracts)
                    if (con.status == true)
                    {
                        lDis.Area = con.area;
                        lDis.Dis_Type = con.disType;
                        break;
                    }
                listDis.Add(lDis);
            }
            logger.Info("End service...");
            return listDis;
        }

        public Distributor SearchByID(int id )
        {
            logger.Info("Start service....");
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Distributor> repo = uow.Repository<Distributor>();
            Distributor dis = repo.Get(x => x.idDistributor == id);
            logger.Info("End service...");
            return dis;
        }

        public bool UpdateDebt(int id, long money)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int id, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
