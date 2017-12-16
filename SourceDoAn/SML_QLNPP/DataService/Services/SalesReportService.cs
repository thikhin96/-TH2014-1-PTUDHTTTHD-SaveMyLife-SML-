using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Interfaces;
using DataService.Interfaces;
using DataModel;
using NLog;
namespace DataService
{
    public class SalesReportService : ISalesReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public SalesReportService(IUnitOfWork unitOfWork)
        {
            //_SaleReportRepository = unitOfWork.Repository<SaleReport>();
            _unitOfWork = unitOfWork;
        }
        public SalesReport GetSalesReport(int id)
        {
            IRepository<SalesReport> repository = _unitOfWork.Repository<SalesReport>();
            var result = repository.Get(a => a.idSalesReport == id);
            return result != null ? result : null;
        }

        public IList<SalesReport> Search(int idSalesReport, string createDate, string endDate)
        {
            try
            {
                IRepository<SalesReport> repository = _unitOfWork.Repository<SalesReport>();
                if (idSalesReport == 0 && createDate == "" && endDate == "")
                {
                    return repository.GetAll().ToList();
                }
                if (idSalesReport > 0)
                {

                    return repository.GetAll(a => a.idSalesReport == idSalesReport).ToList();

                }
                else
                {

                    if (createDate != "" && endDate != "")
                    {
                        var sdate = Convert.ToDateTime(createDate);
                        var edate = Convert.ToDateTime(endDate);
                        return repository.GetAll(a => (a.beginDate.Value.Year == sdate.Year
                                            && a.beginDate.Value.Month == sdate.Month
                                            && a.beginDate.Value.Day == sdate.Day)
                                            &&
                                            (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                    else if (createDate != "")
                    {
                        var sdate = Convert.ToDateTime(createDate);
                        return repository.GetAll(a => (a.beginDate.Value.Year == sdate.Year
                                            && a.beginDate.Value.Month == sdate.Month
                                            && a.beginDate.Value.Day == sdate.Day)).ToList();
                    }
                    else
                    {
                        var edate = Convert.ToDateTime(endDate);
                        return repository.GetAll(a => (a.endDate.Value.Year == edate.Year
                                            && a.endDate.Value.Month == edate.Month
                                            && a.endDate.Value.Day == edate.Day)).ToList();
                    }
                }

            }
            catch
            {
                return null;
            }
        }

    }
}
