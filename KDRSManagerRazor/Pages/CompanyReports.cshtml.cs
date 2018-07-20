using System;
using System.Collections.Generic;
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

        public void OnGet()
        {
            //string tmp = Request.QueryString.Value;
            string _id = Request.Query["id"];
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