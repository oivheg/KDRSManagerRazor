﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KDRSManagerRazor.Pages
{
    public class ServerModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your application description page.";
        }

        public void Add(ServerModel srv)
        {
            string tmp = "tmp";
        }

        public void BtnClicked()
        {
        }
    }
}