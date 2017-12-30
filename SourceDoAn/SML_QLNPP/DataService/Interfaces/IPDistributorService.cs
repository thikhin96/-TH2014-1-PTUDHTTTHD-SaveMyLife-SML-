﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IPDistributorService
    {
        bool Create(PotentialDistributor pDis);
        bool Create(PotentialDistributor pDis, Representative rep);
        bool UpdateStatus(int id, byte status, string note);
        IList<PotentialDistributor> SearchByStatus(byte status);
        IList<PotentialDistributor> GetAll();
        PotentialDistributor SearchByID(int id);
        int GenerateOrderId();
    }
}