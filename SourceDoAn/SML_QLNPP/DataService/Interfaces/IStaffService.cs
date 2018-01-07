
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
        List<Staff> getAll();
        Staff getStaff(int id);
        Staff GetByAccount(string account);
    }
}
