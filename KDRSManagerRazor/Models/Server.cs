using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSManagerRazor.Models
{
    public class Server
    {
        //public Server(string srv, int user, string pw)
        //{
        //    UserID = user;
        //    Adress = srv;
        //    Password = pw;
        //}

        public int UserID { get; set; }
        public String Adress { get; set; }
        public String Password { get; set; }
    }
}