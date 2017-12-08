using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace DataService.Dtos
{
    public class DistributorList
    {
        private DistributorBase dis;
        private string area;
        private bool? dis_Type;

        public bool? Dis_Type
        {
            get
            {
                return dis_Type;
            }

            set
            {
                dis_Type = value;
            }
        }

        public DistributorBase Dis
        {
            get
            {
                return dis;
            }

            set
            {
                dis = value;
            }
        }

        public string Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }
    }
}