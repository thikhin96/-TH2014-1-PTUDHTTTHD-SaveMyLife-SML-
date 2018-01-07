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
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public DebtService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _debtRepository = unitOfWork.Repository<Debt>();
        }

        public int GenerateDebtId()
        {
            var latestDebt = _debtRepository.GetAll().OrderByDescending(x => x.idDebt).FirstOrDefault();
            if (latestDebt != null)
                return latestDebt.idDebt + 1;
            else
                return 1;
        }

        public string CreateDebt(Debt debt)
        {
            try
            {
                var announcement = "thanh cong";
                _debtRepository.Add(debt);
                _unitOfWork.SaveChange();
                return announcement;
            }
            catch (Exception ex)
            {
                return "Không thể tạo phiếu công nợ!";
            }
        }

        public Debt GetDebt(int debtId)
        {
            try
            {
                var request = _debtRepository.Get(x => x.idDebt == debtId);
                return request;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
