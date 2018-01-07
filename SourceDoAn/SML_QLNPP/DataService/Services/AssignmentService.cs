using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Interfaces;
using DataService.Interfaces;
using DataModel;
using DataService.Dtos;
using NLog;

namespace DataService.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Assignment> _assignmentRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public AssignmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _assignmentRepository = unitOfWork.Repository<Assignment>();
        }

        public Assignment getAssigment(int idStaff, int idDis)
        {
            IRepository<Assignment> repository = _unitOfWork.Repository<Assignment>();
            try
            {
                var assig = repository.GetAll(x => (x.staff == idStaff && x.PDistributor == idDis)).ToList().FirstOrDefault();
                return assig;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateAssignment(Assignment assig)
        {
            logger.Info("Start Create assigment method");
            try
            {
                logger.Info("Status: Success");
                _assignmentRepository.Add(assig);
                _unitOfWork.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return false;
            }
        }

        public bool DeleteAssignment(Assignment assig)
        {
            logger.Info("Start Delete assigment method");
            try
            {
                logger.Info("Status: Success");
                _assignmentRepository.Delete(assig);
                _unitOfWork.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return false;
            }
        }
    }
}
