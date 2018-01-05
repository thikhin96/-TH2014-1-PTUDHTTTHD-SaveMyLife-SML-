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
    public class DebtService :IDebtService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Debt> _debtRepository;
        private readonly IRepository<ReturnBase> _returnRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public DebtService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _debtRepository = unitOfWork.Repository<Debt>();
        }

        public int GenerateDebtId()
        {
            _logger.Info("Start Generating Debt id");
            var latestDebt = _debtRepository.GetAll().OrderByDescending(x => x.idDebt).FirstOrDefault();
            _logger.Info("Success: Debt id generated!");
            if (latestDebt != null)
                return latestDebt.idDebt + 1;
            else
                return 1;
        }

        public string CreateDebt(Debt debt)
        {
            try
            {
                _logger.Info("Start creating new debt");
                var announcement = "thanh cong";
                _debtRepository.Add(debt);
                _unitOfWork.SaveChange();
                _logger.Info("Success: A new debt created!");
                return announcement;
            }
            catch (Exception ex)
            {
                _logger.Error("Error creating new debt: {0}", ex.Message);
                return "Không thể tạo phiếu công nợ!";
            }
        }
        public ReturnBase GetReturn(int returnId)
        {
            try
            {
                var request = _returnRepository.Get(x => x.idReturn == returnId);
                return request;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
