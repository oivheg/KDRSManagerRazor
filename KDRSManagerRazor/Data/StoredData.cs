using Hanssens.Net;
using KDRSManagerRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDRSManagerRazor.Data
{
    public static class StoredData
    {
        private static List<Company> Companies = new List<Company>();
        private static List<Report> Reports = new List<Report>();
        public static List<Server> Servers = new List<Server>();

        private static LocalStorageConfiguration config = new LocalStorageConfiguration()
        {
            // see the section "Configuration" further on
            AutoLoad = false,
            AutoSave = false
        };

        private static LocalStorage storage = new LocalStorage(config);

        public static List<Company> GetCompanies()
        {
            return Companies;
        }

        public static void SetCompanies(List<Company> list)
        {
            foreach (Company comp in list)
            {
                Companies.Add(comp);
            }
            //Companies = list;
        }

        private static List<String> storageKeys = new List<string>();

        internal static void SetServer(Server srv)
        {
            //Servers.Add(srv);

            // store any object, or collection providing only a 'key'
            string key = srv.Adress;
            string value = srv.Adress + ";" + srv.UserID + ";" + srv.Password;
            storage.Store(key, value);
            storageKeys.Add(key);
        }

        internal static List<Server> GetServers()
        {
            //// fetch any object - as object
            //var skey = storage.Get(key);
            //return skey.ToString();
            //String key = "Server1";
            Servers.Clear();

            foreach (String item in storageKeys)
            {
                String Server = (string)storage.Get(item);

                String ip, pw;
                String user;
                string[] words = Server.Split(";");

                ip = words.GetValue(0).ToString();
                user = words.GetValue(1).ToString();
                pw = words.GetValue(2).ToString();
                Server TMP = new Server
                {
                    Adress = ip,
                    UserID = int.Parse(user),
                    Password = pw
                };
                Servers.Add(TMP);
            }

            return Servers;
        }

        public static void SetReports()
        {
            Reports.Clear();
            List<Report> data = new List<Report>();

            Reports.Add(new Report(1, "Salgs Rapport Avdeling", "SaleReportD"));
            Reports.Add(new Report(4, "Salgs Rapport Ansatt", "SaleReportA"));
            Reports.Add(new Report(3, "Salgs Rapport Time", "SaleReportT"));
        }

        internal static List<Report> GetReports()
        {
            return Reports;
        }
    }
}