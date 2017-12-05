using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.Interfaces;
using DataService;
using NLog;
using DataModel.Interfaces;

namespace DataService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public OrderService(IUnitOfWork unitOfWork)
        {
            //_promotionRepository = unitOfWork.Repository<Promotion>();
            _unitOfWork = unitOfWork;
        }
        public int AddOrder(DonDatHang order)
        {
            throw new NotImplementedException();
        }

        public bool CheckDept(int idDistributor)
        {
            throw new NotImplementedException();
        }

        public IList<DonDatHang> SearchOrder(string keyword, string createDate, int status)
        {
            try
            {
                IRepository<DonDatHang> repository = _unitOfWork.Repository<DonDatHang>();
                if(createDate=="" || createDate == null)
                {
                    if (keyword != null && keyword != "")
                    {
                        return repository.GetAll(a => (a.NhanVien.TenNV.Contains(keyword) || a.NhaPhanPhoi.TenNPP.Contains(keyword) || a.NguoiLienHeGiaoHang.HoTen.Contains(keyword)) && a.TinhTrang == status).ToList();
                    }
                    else
                    {
                        return repository.GetAll(a => a.TinhTrang == status).ToList();
                    }
                }
                else
                {
                    var date = Convert.ToDateTime(createDate);
                    if (keyword != null && keyword != "")
                    {
                        return repository.GetAll(a => (a.NhanVien.TenNV.Contains(keyword) || a.NhaPhanPhoi.TenNPP.Contains(keyword) || a.NguoiLienHeGiaoHang.HoTen.Contains(keyword)) && (a.NgayLap.Value.Year == date.Year
                                            && a.NgayLap.Value.Month == date.Month
                                            && a.NgayLap.Value.Day == date.Day) && a.TinhTrang == status).ToList();
                    }
                    else
                    {
                        return repository.GetAll(a => (a.NgayLap.Value.Year == date.Year
                                            && a.NgayLap.Value.Month == date.Month
                                            && a.NgayLap.Value.Day == date.Day) && a.TinhTrang == status).ToList();
                    }
                }
                
            }
            catch
            {
                return null;
            }
        }

        public int UpdateOrder(DonDatHang order)
        {
            throw new NotImplementedException();
        }

        public DonDatHang GetOrder(int id)
        {
            IRepository<DonDatHang> repository = _unitOfWork.Repository<DonDatHang>();
            var result = repository.Get(a => a.ID_DonHang == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
