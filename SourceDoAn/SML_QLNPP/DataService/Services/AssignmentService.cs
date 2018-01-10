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

        public Assignment GetAssignment(int idStaff, int idDis)
        {
            logger.Info("Start to get an assignment");
            try
            {
                var assig = _assignmentRepository.GetAll(x => (x.staff == idStaff && x.PDistributor == idDis)).ToList().FirstOrDefault();
                logger.Info("Status: Success");
                return assig;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return null;
            }
        }

        public bool CreateAssignment(Assignment assig)
        {
            logger.Info("Start Create assigment method");
            try
            {
                _assignmentRepository.Add(assig);
                _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return false;
            }
            logger.Info("Status: Success");
            return true;
        }

        public bool DeleteAssignment(Assignment assig)
        {
            logger.Info("Start Delete assigment method");
            try
            {
                _assignmentRepository.Delete(assig);
                _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return false;
            }
            logger.Info("Status: Success");
            return true;
        }
    }
}
