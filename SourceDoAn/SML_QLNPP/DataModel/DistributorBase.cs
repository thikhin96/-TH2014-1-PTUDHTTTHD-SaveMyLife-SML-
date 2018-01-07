using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DistributorBase
    {
        public virtual int idDistributor { get; set; }
        public virtual string name { get; set; }
        public virtual string address { get; set; }
        public virtual string phone { get; set; }
        public virtual string Email { get; set; }
        public virtual Nullable<System.DateTime> createdDate { get; set; }
        public virtual Nullable<System.DateTime> updatedDate { get; set; }
        public virtual string note { get; set; }

        public static implicit operator DistributorBase(Distributor v)
        {
            throw new NotImplementedException();
        }
    }
}
