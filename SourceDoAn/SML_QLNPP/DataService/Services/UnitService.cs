﻿using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class UnitService: IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Unit> _unitRepository;

        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitRepository = _unitOfWork.Repository<Unit>();
        }
        public List<Unit> GetAllUnit()
        {
            try
            {
                var units = _unitRepository.GetAll().ToList();
                return units;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
