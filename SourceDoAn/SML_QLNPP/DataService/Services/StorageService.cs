using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Interfaces;
using DataService;
using NLog;
using DataModel.Interfaces;

namespace DataService.Services
{
    public class StorageService : IStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Storage> _storageRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public StorageService(IUnitOfWork unitOfWork, IDistributorService distributorService)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = unitOfWork.Repository<Storage>();
        }

        public List<Storage> SearchStorage(int id)
        {
            try
            {
                IRepository<Storage> repository = _unitOfWork.Repository<Storage>();
                if (id != null)
                {
                    return repository.GetAll(a => (a.IdStorage == id)).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Storage> GetAllStorages()
        {
            try
            {
                var storages = _storageRepository.GetAll().ToList();
                return storages;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Storage GetStorage(int id)
        {
            IRepository<Storage> repository = _unitOfWork.Repository<Storage>();
            var result = repository.Get(a => a.IdStorage == id);
            return result != null ? result : null;
        }
    }
}
