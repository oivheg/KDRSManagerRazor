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
    public class ReportModel : PageModel
    {
        public String Name = "Not set";
        private List<Report> LstRp;

        public void OnGet()
        {
            LstRp = null;
            //string tmp = Request.QueryString.Value;
            string _id = Request.Query["id"];

            LstRp = StoredData.GetReports();

            foreach (var comp in LstRp)
            {
                if (comp.Id == int.Parse(_id))
                {
                    Name = comp.DisplayName;
                }
            }
        }
    }
}