using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;


namespace DataService.Interfaces
{
    public interface IReturnRequestService
    {
        bool CheckDept(int idDistributor);
        IList<ReturnRequest> SearchReturnRequest(string keyword, int status);
        int AddReturnRequest(ReturnRequest returnRequest);
        string UpdateReturnRequest(ReturnRequest returnRequest);
        ReturnRequest GetReturnRequest(int id);
        int GenerateReturnRequestId();
        string CreateReturnRequest(ReturnRequest returnRequest);
    }
}
