using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Dtos;

namespace SML_QLNPP.Models
{
    public class ContractListViewModel
    {
        public IList<ContractList> listCon;
        public string keyword { get; set; }
        public int criterion { get; set; }
    }

}