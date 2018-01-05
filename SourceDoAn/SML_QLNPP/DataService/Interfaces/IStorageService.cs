using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IStorageService
    {
        List<Storage> SearchStorage(int id);
        List<Storage> GetAllStorages();
        Storage GetStorage(int id);
    }
}
