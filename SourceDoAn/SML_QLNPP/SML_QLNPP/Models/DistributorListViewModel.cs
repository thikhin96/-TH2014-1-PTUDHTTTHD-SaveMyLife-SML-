using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Dtos;
//using System.ComponentModel.DataAnnotations;

namespace SML_QLNPP.Models
{
    public class DistributorListViewModel
    {
        public string id { get; set; }
        public IList<DistributorList> listDis;
    }
}