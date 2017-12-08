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

namespace DataService.Services
{
    public class DistributorService : IDistributorService
    {
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

        public IList<DistributorList> GetAll()
        {
            IList<Distributor> ds_Dis = new List<Distributor>();
            IUnitOfWork uow = new UnitOfWork();
            IRepository<Distributor> repo = uow.Repository<Distributor>();
            ds_Dis = repo.GetAll().ToList();
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
            return listDis;
        }

        public DistributorBase SearchByID(int id)
        {
            throw new NotImplementedException();
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
