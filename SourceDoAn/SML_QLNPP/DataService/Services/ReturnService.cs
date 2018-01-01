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
            _logger.Info("Start searching for return cards with key word: " + keyWord + "and trade form: " + tradeForm.ToString());
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
                _logger.Info("Searching for return cards encountered error: " +  ex.Message);
                return new List<ReturnBase>();
            }
            
            
        }

        public ReturnBase GetReturnCardDetail(int cardId)
        {
            _logger.Info("Start Getting return card detail number: " + cardId.ToString());
            try
            {
                var detail = _returnRepository.Get(x => x.idReturn == cardId);
                _logger.Info("Success: Return Card number " + cardId.ToString() + "returned!");
                return detail;
                
            }
            catch (Exception ex)
            {
                _logger.Info("Error Getting return card" + ex.Message);
                return null;
            }
            
        }

        public ReturnRequest GetReturnRequest(int returnRequestId)
        {
            try
            {
                var request = _returnRequestRepository.Get(x => x.IdReturnRequest == returnRequestId);
                return request;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
