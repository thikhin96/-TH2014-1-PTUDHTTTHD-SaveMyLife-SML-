﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Interfaces;
using DataService.Interfaces;
using DataModel;
using NLog;

namespace DataService.Services
{
    public class PDistributorService : IPDistributorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PotentialDistributor> _pdistributorRepository;
        private readonly IRepository<Representative> _representativeRepository;
        private readonly IRepository<Assignment> _assignmentRepository;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public PDistributorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pdistributorRepository = unitOfWork.Repository<PotentialDistributor>();
            _representativeRepository = unitOfWork.Repository<Representative>();
            _assignmentRepository = unitOfWork.Repository<Assignment>();
        }


        public bool Create(PotentialDistributor pDis, Representative rep)
        {
            logger.Info("Start Create potential distributor method");
            try
            {
                _pdistributorRepository.Add(pDis);
                _representativeRepository.Add(rep);
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

        public bool Create(PotentialDistributor pDis)
        {
            logger.Info("Start Create potential distributor method");
            try
            {
                _pdistributorRepository.Add(pDis);
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


        public PotentialDistributor GetPDistributor(int id)
        {
            logger.Info("Start Search potential distributor by idDistributor method");
            try
            {
                IRepository<PotentialDistributor> repository = _unitOfWork.Repository<PotentialDistributor>();
                var result = repository.Get(x => x.idDistributor == id);
                if (result != null)
                {
                    logger.Info("Status: Success");
                    return result;
                }
                else
                {
                    logger.Info("Status: Success");
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return null;
            }
        }

        public IList<PotentialDistributor> SearchByStatus(byte status)
        {
            logger.Info("Start Search potential distributor by status ...");
            try
            {
                IRepository<PotentialDistributor> repository = _unitOfWork.Repository<PotentialDistributor>();
                var result = repository.GetAll(x => x.status == status);
                if (result != null)
                {
                    logger.Info("Status: Success");
                    return result.ToList();
                }
                else
                {
                    logger.Info("Status: Success");
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return null;
            }
        }

        public bool UpdateStatus(int id, byte status, string note)
        {
            logger.Info("Start update status of potential distributor...");
            try
            {
                PotentialDistributor pDis = GetPDistributor(id);
                pDis.status = status;
                pDis.note = note;
                _pdistributorRepository.Update(pDis);
                _unitOfWork.SaveChange();
                logger.Info("Status: Success");
                return true;
             
            }
            catch (Exception ex)
            {
                logger.Info("Status: Fail + " + ex.Message);
                return false;
            }
        }

        public int GeneratePDistributorId()
        {
            var latestOrder = _pdistributorRepository.GetAll().OrderByDescending(x => x.idDistributor).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idDistributor + 1;
            else
                return 1;
        }

        public bool UpdatePDistributor(PotentialDistributor pdis)
        {
            logger.Info("Start Update potential distributor method");
            try
            {
                _pdistributorRepository.Update(pdis);
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
