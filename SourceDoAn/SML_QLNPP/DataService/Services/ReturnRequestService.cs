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
    public class ReturnRequestService : IReturnRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ReturnRequest> _returnRequestRepository;
        private readonly IRepository<Distributor> _distributorRepository;
        private readonly IRepository<ReturnRequestDetail> _returnRequestDetailRepository;
        private readonly IDistributorService _distributorService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public ReturnRequestService(IUnitOfWork unitOfWork, IDistributorService distributorService)
        {
            _unitOfWork = unitOfWork;
            _returnRequestRepository = unitOfWork.Repository<ReturnRequest>();
            _distributorRepository = unitOfWork.Repository<Distributor>();
            _returnRequestDetailRepository = unitOfWork.Repository<ReturnRequestDetail>();
            _distributorService = distributorService;
        }
        public int AddReturnRequest(ReturnRequest returnRequest)
        {
            throw new NotImplementedException();
        }

        public bool CheckDept(int idDistributor)
        {
            throw new NotImplementedException();
        }

        public IList<ReturnRequest> SearchReturnRequest(string keyword, int status)
        {
            try
            {
                IRepository<ReturnRequest> repository = _unitOfWork.Repository<ReturnRequest>();
                if (keyword != null && keyword != "")
                {
                    return repository.GetAll(a => (a.Distributor1.name.Contains(keyword)) && a.Status == status).ToList();
                }
                else
                {
                    return repository.GetAll(a => a.Status == status).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    
        public string UpdateReturnRequest(ReturnRequest returnRequest)
        {
            try
            {
                _returnRequestRepository.Update(returnRequest);
                _unitOfWork.SaveChange();
                return "ok";
            }
            catch
            {
                return "not";
                throw;
            }
        }

        public ReturnRequest GetReturnRequest(int id)
        {
            IRepository<ReturnRequest> repository = _unitOfWork.Repository<ReturnRequest>();
            ReturnRequest result;
            try
            {
                result = repository.Get(a => a.IdReturnRequest == id);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        public int GenerateReturnRequestId()
        {
            var latestReturnRequest = _returnRequestRepository.GetAll().OrderByDescending(x => x.IdReturnRequest).FirstOrDefault();
            if (latestReturnRequest != null)
                return latestReturnRequest.IdReturnRequest + 1;
            else
                return 0;
        }
        public string CreateReturnRequest(ReturnRequest returnRequest)
        {
            try
            {
                if (_distributorRepository.Get(x => x.idDistributor == returnRequest.Distributor) != null)
                {

                    _returnRequestRepository.Add(returnRequest);
                    _unitOfWork.SaveChange();
                }
                else
                {
                    return "Nhà phân phối không tồn tại";
                }
                return "thanh cong";
            }
            catch (Exception ex)
            {
                return "Không thể tạo đơn yêu cầu đổi trả";
                throw;
            }
        }
    }
}
