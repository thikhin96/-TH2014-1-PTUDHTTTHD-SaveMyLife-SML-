using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DataService.Services
{
    public class StaffService: IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Staff> _staffRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public StaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = _unitOfWork.Repository<Staff>();
        }
        public List<Staff> getAll()
        {
            try
            {
                var staff = _staffRepository.GetAll().ToList();
                return staff;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Staff getStaff(int id)
        {
            try
            {
                var staff = _staffRepository.Get(x=> x.idStaff == id);
                return staff;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
