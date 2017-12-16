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
        IList<SalesReport> Search(int idSaleReport, string createDate, string endDate);
        SalesReport GetSalesReport(int id);
    }
}
