using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSManagerRazor.Models
{
    public class Company
    {
        private static readonly Company instance = new Company();
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Server { get; set; }
    }
}