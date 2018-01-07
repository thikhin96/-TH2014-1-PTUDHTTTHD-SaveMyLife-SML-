using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using NLog;

namespace DataService.Services
{
    public class PaySlipService : IPaySlipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PaySlip> _paySlipRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PaySlipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paySlipRepository = unitOfWork.Repository<PaySlip>();
        }

        public int GeneratePaySlipId()
        {
            var latestDebt = _paySlipRepository.GetAll().OrderByDescending(x => x.idPaySlip).FirstOrDefault();
            if (latestDebt != null)
                return latestDebt.idPaySlip + 1;
            else
                return 1;
        }

        public string CreatePaySlip(PaySlip paySlip)
        {
            try
            {
                var announcement = "thanh cong";
                _paySlipRepository.Add(paySlip);
                _unitOfWork.SaveChange();
                return announcement;
            }
            catch (Exception ex)
            {
                return "Không thể tạo phiếu công nợ!";
            }
        }
    }
}
