using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface ILogProductService
    {
        bool Add(Log_Product log_p);
    }
}
