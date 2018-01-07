using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dtos
{
    public class ContractList
    {
        int idCon;
        string dis_Name;
        DateTime? beginDate;
        DateTime? expriedDate;
        bool? status;

        public int IdCon
        {
            get
            {
                return idCon;
            }

            set
            {
                idCon = value;
            }
        }

        public string Dis_Name
        {
            get
            {
                return dis_Name;
            }

            set
            {
                dis_Name = value;
            }
        }

        public DateTime? BeginDate
        {
            get
            {
                return beginDate;
            }

            set
            {
                beginDate = value;
            }
        }

        public DateTime? ExpriedDate
        {
            get
            {
                return expriedDate;
            }

            set
            {
                expriedDate = value;
            }
        }

        public bool? Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
