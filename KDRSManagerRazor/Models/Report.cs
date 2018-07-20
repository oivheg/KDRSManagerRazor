using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSManagerRazor.Models
{
    public class Report
    {
        public Report(int _id, string _DpName, string _RpName)
        {
            Id = _id;
            ReportName = _RpName;
            DisplayName = _DpName;
        }

        public int Id { get; set; }
        public String ReportName { get; set; }
        public String DisplayName { get; set; }
    }
}