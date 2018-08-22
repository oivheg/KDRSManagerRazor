using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSMonitorRazor.Models
{
    public class SaleReportD
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String SalesAmount { get; set; }
        public String CostPrice { get; set; }
    }
}