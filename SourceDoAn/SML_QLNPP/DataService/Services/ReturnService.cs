using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ReturnBase> _returnRepository;
        private readonly IRepository<ReturnRequest> _returnRequestRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ReturnService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _returnRepository = unitOfWork.Repository<ReturnBase>();
            _returnRequestRepository = unitOfWork.Repository<ReturnRequest>();
        }

        public List<ReturnBase> GetReturnCards(string keyWord, int tradeForm)
        {
            _logger.Info("Start searching for return cards with key word: {0} and trade form: {1}", keyWord, tradeForm.ToString());
            var realTradeForm = Convert.ToBoolean(tradeForm);
            try
            {
                var cards = _returnRepository.GetAll(c => c.Distributor.name.Contains(keyWord) && (tradeForm == 2 || c.ModeOfReturn == realTradeForm))
                                         .ToList();
                _logger.Info("Searched for return cards successfully");
                return cards;
            }
            catch (Exception ex)
            {
                _logger.Error("Searching for return cards encountered error: {0}",  ex.Message);
                return new List<ReturnBase>();
            }
            
            
        }

        public int GenerateReturnId()
        {
            _logger.Info("Start Generating Return Card id");
            var latestReturn = _returnRepository.GetAll().OrderByDescending(x => x.idReturn).FirstOrDefault();
            _logger.Info("Success: Return Card id generated!");
            if (latestReturn != null)
                return latestReturn.idReturn + 1;
            else
                return 1;
        }

        public ReturnBase GetReturnCardDetail(int cardId)
        {
            _logger.Info("Start Getting return card detail number: {0}", cardId);
            try
            {
                var detail = _returnRepository.Get(x => x.idReturn == cardId);
                _logger.Info("Success: Return Card number {0} returned!", cardId);
                return detail;
                
            }
            catch (Exception ex)
            {
                _logger.Error("Error Getting return card: {0}", ex.Message);
                return null;
            }
            
        }

        public ReturnRequest GetReturnRequest(int returnRequestId)
        {
            _logger.Info("Start Getting return request number: {0}", returnRequestId.ToString());
            try
            {
                var request = _returnRequestRepository.Get(x => x.IdReturnRequest == returnRequestId);
                _logger.Info("Success: Return Request number {0} returned!", returnRequestId);
                return request;
            }
            catch (Exception ex)
            {
                _logger.Info("Error Getting return request {0}", ex.Message);
                return null;
            }
        }

        public string CreateReturnCard(ReturnBase returnCard)
        {
            try
            {
                _logger.Info("Start creating new return card");
                var announcement = "thanh cong";
                _returnRepository.Add(returnCard);
                _unitOfWork.SaveChange();
                _logger.Info("Success: A new return card created!");
                return announcement;
            }
            catch (Exception ex)
            {
                _logger.Error("Error creating new return card: {0}", ex.Message);
                return "Không thể tạo phiếu đổi trả!";
            }
        }
    }
}
