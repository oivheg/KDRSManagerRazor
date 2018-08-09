using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KDRSManagerRazor.Pages
{
    public class ServerModel : PageModel
    {
        [BindProperty]
        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your application description page.";
        }

        public void OnPost(string emailAddress)
        {
            Response.Redirect("http://vg.no");
            // do something with emailAddress
        }

        public void BtnClicked()
        {
        }
    }
}