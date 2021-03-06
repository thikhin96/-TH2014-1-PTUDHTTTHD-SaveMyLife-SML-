﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IDebtService
    {
        int GenerateDebtId();
        string CreateDebt(Debt debt);
        Debt GetDebt(int debtId);
    }
}
