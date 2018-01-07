using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface ISalesReportService
    {
        IList<SalesReport> Search(int idSalesReport, string createDate, string endDate);
        IList<SalesReport> SearchListOrder(int idOrder, int idDistributor, string CreateDate);
        IList<SalesReport> SearchListBill(int idBill,int idDistributor, string CreateDate);
        SalesReport GetSalesReport(int id);
    }
}
