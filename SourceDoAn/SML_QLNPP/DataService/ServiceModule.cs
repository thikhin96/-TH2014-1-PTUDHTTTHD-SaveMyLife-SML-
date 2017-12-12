using DataModel.Interfaces;
using DataModel.Repositories;
using DataService.Services;
using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DataService
{
    public class ServiceModule
    {
        public static void UnityRegister(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDistributorService, DistributorService>();
        }
    }
}
