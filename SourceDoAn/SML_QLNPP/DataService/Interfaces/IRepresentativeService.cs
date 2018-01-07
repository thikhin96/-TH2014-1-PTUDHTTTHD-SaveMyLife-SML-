﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IRepresentativeService
    {
        Representative getRepresentative(int id);
        int Create(Representative person);
        bool CreateRepresentative(Representative person);
        bool UpdateRepresentative(Representative person);
        bool UpdateTypeOfRepresentation(int idRep, int idDis);
        bool CheckPhone(string phone);
        bool CheckEmail(string email);
        int GenerateRepresentativeId();
        Representative GetByID(int id);
        int GenerateOrderId();
    }
}