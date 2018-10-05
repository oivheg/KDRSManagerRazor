using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KDRSManagerRazor.Data;
using KDRSManagerRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KDRSManagerRazor.Pages
{
    public class CompanyReportsModel : PageModel
    {
        public String Name = "Not set";
        public string srv = "no server set";

        public string From, To;

        public void OnGet()
        {
            //string tmp = Request.QueryString.Value;
            string _id = Request.Query["id"];
            srv = Request.Query["srv"];
            //From = DateTime.ParseExact(Request.Query["from"], "d", CultureInfo.InvariantCulture);
            //To = DateTime.ParseExact(Request.Query["to"], "d", CultureInfo.InvariantCulture);
            From = Request.Query["from"];
            To = Request.Query["to"];
            StoredData.SetReports();
            List<Company> cmop = StoredData.GetCompanies();

            foreach (var comp in cmop)
            {
                if (comp.Id == _id)
                {
                    Name = comp.Text;
                }
            }
        }

        public List<Report> ReportList()
        {
            List<Report> tmp = StoredData.GetReports();
            return StoredData.GetReports();
        }
    }
}