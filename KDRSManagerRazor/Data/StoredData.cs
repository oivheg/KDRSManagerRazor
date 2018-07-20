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
        public static List<Server> ServerAdresses = new List<Server>();

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

        internal static void SetServer(Server srv)
        {
            ServerAdresses.Add(srv);
        }

        internal static List<Server> GetServer()
        {
            return ServerAdresses;
        }

        public static void SetReports()
        {
            Reports.Clear();
            List<Report> data = new List<Report>();

            Reports.Add(new Report(1, "Salgs Rapport Avdeling", "SaleReportD"));
            Reports.Add(new Report(2, "Salgs Rapport Ansatt", "SaleReportA"));
            Reports.Add(new Report(3, "Salgs Rapport Time", "SaleReportT"));
        }

        internal static List<Report> GetReports()
        {
            return Reports;
        }
    }
}