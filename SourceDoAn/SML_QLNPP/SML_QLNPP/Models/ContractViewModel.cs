using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Dtos;

namespace SML_QLNPP.Models
{
    public class ContractViewModel
    {
        public IList<ContractList> listCon;
        public string keywork { get; set; }
        public string criterion { get; set; }
    }
}