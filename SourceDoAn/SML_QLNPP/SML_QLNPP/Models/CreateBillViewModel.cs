﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreateBillViewModel
    {
        public int idStaff { get; set; }
        public int idDeliveryOrder { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public string nameDistributor { get; set; }
        public decimal purchase { get; set; }
        public byte types { get; set; }
        public DateTime createdDate { get; set; }
        public string description { get; set; }
    }
}