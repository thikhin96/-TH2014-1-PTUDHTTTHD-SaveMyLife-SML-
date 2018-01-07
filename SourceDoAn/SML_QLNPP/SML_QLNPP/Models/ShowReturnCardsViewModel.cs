using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Models
{
    public class ShowReturnCardsViewModel
    {
        public List<ReturnBase> ReturnCards { get; set; }
        public string KeyWord { get; set; }
        public bool TradeForm { get; set; }
        public List<SelectListItem> TradeFormList
        { get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Đổi",
                        Value = "0"
                    },
                    new SelectListItem
                    {
                        Text = "Trả",
                        Value = "1"
                    },
                    new SelectListItem
                    {
                        Text = "Cả hai",
                        Value = "2"
                    }
                };
            } set
            {

            }
        }
    }
}