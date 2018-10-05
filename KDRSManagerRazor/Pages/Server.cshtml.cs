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
    public class ServerModel : PageModel
    {
        [BindProperty]
        public String ip { get; set; }

        [BindProperty]
        public int user { get; set; }

        [BindProperty]
        public String pw { get; set; }

        [BindProperty]
        public String comment { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Leg til din KDRGold Server";
        }

        //public void OnPost(string inputserver)
        //{
        //    Response.Redirect("http://vg.no" + inputserver);
        //    // do something with emailAddress
        //}

        //public IActionResult OnPost()
        //{
        //    Server srv = new Server(ip, user, pw);
        //    StoredData.SetServer(srv);
        //    ModelState.Clear();
        //    return Page();
        //}

        public string BtnClicked()
        {
            return "text";
        }

        public void getServersFromTable()
        {
        }

        public List<Server> ServerList()
        {
            List<Server> Servers = StoredData.GetServers();

            return Servers;
        }
    }
}