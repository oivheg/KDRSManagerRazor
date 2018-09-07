using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using KDRSManagerRazor.Connections;
using KDRSManagerRazor.Data;
using KDRSManagerRazor.Models;
using KDRSMonitorRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KDRSManagerRazor.Pages
{
    public class ReportModel : PageModel
    {
        public String Name = "Not set";
        public String ID = "0";
        public string _srv = "not set";
        private List<Report> LstRp;
        public string From, To;

        public void OnGet()
        {
            LstRp = null;
            //string tmp = Request.QueryString.Value;
            string _id = Request.Query["id"];
            _srv = Request.Query["srv"];
            From = Request.Query["from"];
            To = Request.Query["to"];
            ID = _id;

            LstRp = StoredData.GetReports();

            foreach (var comp in LstRp)
            {
                if (comp.Id == int.Parse(_id))
                {
                    Name = comp.DisplayName;
                }
            }
        }

        private WebRequest wb = new WebRequest();

        public async Task<JsonResult> OnGetReportAsync(String Adress, String UserID, String Password, String id, String from = "Empty", String to = "")
        {
            Server srv = new Server
            {
                Adress = Adress,
                UserID = int.Parse(UserID),
                Password = Password
            };
            int ReportType = int.Parse(id);
            List<object> tmp = (await LoadXMLReportAsync(await wb.GetXml(srv.Adress, srv.UserID, srv.Password, ReportType.ToString(), from).ConfigureAwait(false), ReportType));

            return new JsonResult(tmp);
        }

        private static async Task<List<object>> LoadXMLReportAsync(String xml, int ReportType)
        {
            List<object> rawData = null;
            await Task.Factory.StartNew(delegate
            {
                XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
                XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
                System.Xml.Linq.XDocument doc = XDocument.Parse(xml);

                if (ReportType == 1 || ReportType == 2 || ReportType == 5)
                {
                    IEnumerable<object> Companies = from s in doc.Descendants().Where(x => x.Name.LocalName == "properties")

                                                    select new SaleReportD
                                                    {
                                                        ID = s.Element(d + "ID").Value,
                                                        //SortId = int.Parse(s.Element(d + "Name").Value),
                                                        Name = s.Element(d + "Name").Value,
                                                        SalesAmount = (s.Element(d + "SalesAmount").Value),
                                                        CostPrice = (s.Element(d + "CostPrice").Value)
                                                    };
                    //var sortedCars = Companies.OrderBy(c => c.SortId);
                    //List<object> myAnythingList = (sortedCars as IEnumerable<object>).Cast<object>().ToList();
                    rawData = Companies.ToList();
                }
                if (ReportType == 3)
                {
                    IEnumerable<object> Companies = from s in doc.Descendants().Where(x => x.Name.LocalName == "properties")

                                                    select new SaleReportA
                                                    {
                                                        ID = int.Parse(s.Element(d + "ID").Value),
                                                        Name = s.Element(d + "Name").Value,
                                                        SalesAmount = (s.Element(d + "SalesAmount").Value),
                                                        CostPrice = (s.Element(d + "CostPrice").Value)
                                                    };
                    rawData = Companies.ToList();
                }
                if (ReportType == 4)
                {
                    IEnumerable<object> Companies = from s in doc.Descendants().Where(x => x.Name.LocalName == "properties")

                                                    select new SaleReportA
                                                    {
                                                        ID = int.Parse(s.Element(d + "ID").Value),
                                                        Name = s.Element(d + "Name").Value,
                                                        SalesAmount = (s.Element(d + "SalesAmount").Value),
                                                        CostPrice = (s.Element(d + "CostPrice").Value)
                                                    };
                    rawData = Companies.ToList();
                }

                if (ReportType == 5)
                {
                    IEnumerable<SalereportT> Companies = from s in doc.Descendants().Where(x => x.Name.LocalName == "properties")

                                                         select new SalereportT
                                                         {
                                                             ID = s.Element(d + "ID").Value,
                                                             SortId = int.Parse(s.Element(d + "Name").Value),
                                                             Name = s.Element(d + "Name").Value,
                                                             SalesAmount = (s.Element(d + "SalesAmount").Value),
                                                             CostPrice = (s.Element(d + "CostPrice").Value)
                                                         };
                    var sortedCars = Companies.OrderBy(c => c.SortId);
                    List<object> myAnythingList = (sortedCars as IEnumerable<object>).Cast<object>().ToList();
                    rawData = myAnythingList.ToList();
                }
            });

            return rawData;
        }
    }
}