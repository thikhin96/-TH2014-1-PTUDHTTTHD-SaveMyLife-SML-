using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class ExceedingDebtException : Exception
    {
        public ExceedingDebtException(string message): base(message) 
        {
            //When distributor's debt is larger than his max debt
        }
    }
}
