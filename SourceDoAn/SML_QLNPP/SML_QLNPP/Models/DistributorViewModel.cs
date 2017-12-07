using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Dtos;

namespace SML_QLNPP.Models
{
    public class DistributorViewModel
    {
        public string id;
        public IList<DistributorList> listDis;
    }
}