using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class DistributorService : IDistributorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Distributor> _distributorRepository;
        private readonly IRepository<Contract> _contractRepository;

        public DistributorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _distributorRepository = unitOfWork.Repository<Distributor>();
            _contractRepository = unitOfWork.Repository<Contract>();
        }

        public bool hasContract(int distributorId)
        {
            var allContracts = _contractRepository.GetAll(x => x.distributor == distributorId);
            if (allContracts.Count() == 0 || allContracts.All(c => c.expiredDate < DateTime.Now))
                return false;
            return true;
        }

        public bool priceOverDebt(int distributorId, decimal price)
        {
            if (_contractRepository.Get(x => x.distributor == distributorId && x.beginDate <= DateTime.Now && x.expiredDate > DateTime.Now).maxDebt < price)
                return true;
            return false;

        }
    }
}
