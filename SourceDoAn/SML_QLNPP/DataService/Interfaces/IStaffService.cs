

﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface IStaffService
    {
        IList<Staff> GetAll();
        Staff GetByAccount(string Account);
        List<Staff> getAll();
        Staff GetStaff(int id);
    }
}
