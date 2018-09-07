using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSMonitorRazor.Models
{
    public class SalereportT
    {
        public string ID { get; set; }
        public int SortId { get; set; }
        public String Name { get; set; }
        public String SalesAmount { get; set; }
        public String CostPrice { get; set; }
    }
}