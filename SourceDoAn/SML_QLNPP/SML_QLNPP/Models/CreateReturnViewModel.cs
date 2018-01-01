using DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SML_QLNPP.Models
{
    public class CreateReturnViewModel : ReturnDetailViewModel
    {
        public List<Product> Products { get; set; }
        public string ActionName;
        public List<SelectListItem> TradeFormList
        {
            get
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
                    }
                };
            }
            set
            {

            }
        }
    }
}