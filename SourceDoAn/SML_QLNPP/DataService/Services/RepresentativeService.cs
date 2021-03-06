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
    public class RepresentativeService : IRepresentativeService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Representative> _representativeRepository;
        
        public RepresentativeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _representativeRepository = _unitOfWork.Repository<Representative>();
        }

        public Representative GetRepresentative(int id)
        {
            logger.Info("Start to get a representative");
            try
            {
                var res = _representativeRepository.Get(x => x.idRepresentative == id);
                logger.Info("Status: Success");
                return res;
            }
            catch (Exception ex)
            {
                logger.Info("Status: Success + " + ex.Message);
                return null;
            }
        }

        public int Create(Representative person)
        {
            logger.Info("Start to create a representative...");
            person.idRepresentative = GenerateRepresentativeId();
            try
            {
                _representativeRepository.Add(person);
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
            return person.idRepresentative;
        }

        public bool CreateRepresentative(Representative person)
        {
            logger.Info("Start add representative method");
            try
            {
                _representativeRepository.Add(person);
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


        public bool UpdateRepresentative(Representative person)
        {
            logger.Info("Start to update a representative...");
            try
            {
                _representativeRepository.Update(person);
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
        public bool UpdateTypeOfRepresentation(int idRep,int idDis)
        {
            logger.Info("Start to update...");
            try
            {
                Representative rep = GetByID(idRep);
                rep.PDistributor = null;
                rep.Distributor = idDis;

                _representativeRepository.Update(rep);
                _unitOfWork.SaveChange();
                logger.Info("End: Successfull...");
                return true;
            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                return false;
            }
        }

        public int GenerateRepresentativeId()
        {
            var latestOrder = _representativeRepository.GetAll().OrderByDescending(x => x.idRepresentative).FirstOrDefault();
            if (latestOrder != null)
                return latestOrder.idRepresentative + 1;
            else
                return 1;
        }

        public Representative GetByID(int id)
        {
            logger.Info("Start to get id of a representative...");
            try
            {
                return _representativeRepository.Get(x => x.idRepresentative == id);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                throw ex;
            }
        }
    }
}
