using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using System;

using System.Collections.Generic;
using System.Linq;

using DataModel.Repositories;
using DataService.Dtos;
using NLog;

namespace DataService.Services
{
    public class DistributorService : IDistributorService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Distributor> _distributorRepository;
        private readonly IRepository<Contract> _contractRepository;
        private readonly IRepository<Debt> _debtRepository;
        private readonly IRepository<Storage> _storageRepository;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public DistributorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _distributorRepository = unitOfWork.Repository<Distributor>();
            _contractRepository = unitOfWork.Repository<Contract>();
            _debtRepository = unitOfWork.Repository<Debt>();
            _storageRepository = unitOfWork.Repository<Storage>();
        }

        public bool hasContract(int distributorId)
        {
            var allContracts = _contractRepository.GetAll(x => x.distributor == distributorId);
            if (allContracts.Count() == 0 || allContracts.All(c => c.expiredDate < DateTime.Now))
                return false;
            return true;
        }

        public Contract GetCurrentContract(int distributorId)
        {
            var currentDate = DateTime.Now;
            var contract = _contractRepository.Get(x => x.distributor == distributorId && x.beginDate <= currentDate && currentDate <= x.expiredDate);
            return contract;
        }

        public bool exceedingDebt(int distributorId)
        {
            return true;
        }
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
                lDis.Dis = dis;
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
            //throw new NotImplementedException();
            logger.Info("Start service....");
            try {
                var distributor = this.SearchByID(id);
                distributor.debt = distributor.debt + money;
                _unitOfWork.SaveChange();
                logger.Info("End service...");
                return true;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Info("End service...");
                return false;
            }
        }

        public List<Storage> GetStorages(string keyWord, int distributorId)
        {
            var storages = _storageRepository.GetAll(x => x.Distributor == distributorId
                                                 && (x.HouseNumber_Street.Contains(keyWord) || x.District.Contains(keyWord) || x.Ward_Commune.Contains(keyWord) || x.City.Contains(keyWord)))
                                                 .ToList();
            return storages;
        }

        public bool UpdateStatus(int id, bool status)
        {
            throw new NotImplementedException();
        }

        public IList<DistributorList> GetAll()
        {
            throw new NotImplementedException();
        }


        public List<Distributor> SearchByName(string searchTerm)
        {
            var distributors = _distributorRepository.GetAll(x => x.name.Contains(searchTerm) && x.status == true).ToList();
            return distributors;
        }

    }
}
