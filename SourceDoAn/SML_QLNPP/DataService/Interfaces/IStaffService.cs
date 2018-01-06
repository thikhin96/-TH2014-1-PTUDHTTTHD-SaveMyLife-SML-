using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IStaffService
    {
       // IList<Staff> SearchByTypes(byte type);

        IList<Staff> GetAll();
        Staff GetByAccount(string Account);
    }
}
