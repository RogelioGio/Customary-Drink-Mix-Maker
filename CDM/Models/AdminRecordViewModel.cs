using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDM.Models
{
    public class AdminRecordViewModel
    {
        public List<AdminRecord> AdminRecords { get; set; }
        public List<ProductTB> ProductTB { get; set; }
        public List<OrderTB> OrderTB { get; set; }
        public List<OrderTB> order { get; set; }

    }
}