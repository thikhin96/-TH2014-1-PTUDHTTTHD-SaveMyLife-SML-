using DataModel;
using System.Collections.Generic;

namespace DataService.Interfaces
{
    public interface IReturnService
    {
        List<ReturnBase> GetReturnCards(string keyWord, int tradeForm);
        ReturnBase GetReturnCardDetail(int cardId);
        ReturnRequest GetReturnRequest(int returnRequestId);
        int GenerateReturnId();
        string CreateReturnCard(ReturnBase returnCard);
    }
}
