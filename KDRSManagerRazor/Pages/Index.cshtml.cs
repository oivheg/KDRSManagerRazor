using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hanssens.Net;
using KDRSManagerRazor.Connections;
using KDRSManagerRazor.Data;
using KDRSManagerRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KDRSManagerRazor.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        private WebRequest wb = new WebRequest();

        public async Task<List<Company>> CompanyList()
        {
            List<string> data = new List<string>();

            //data.Add("test1");
            //data.Add("test1");
            //data.Add("test1");
            //data.Add("test1");

            List<Server> Servers = StoredData.GetServers();
            List<Company> list = new List<Company>();
            try
            {
                foreach (var server in Servers)
                {
                    List<Company> tmp = (await LoadXMLCompanies(await wb.GetXml(server.Adress, server.UserID, server.Password, "3").ConfigureAwait(false)));

                    //List<Company> tmp = (await LoadXMLCompanies(await wb.GetXml("91.192.221.234", 999, "Fuglekasser", "3").ConfigureAwait(false)));
                    foreach (var Company in tmp)
                    {
                        list.Add(Company);
                    }
                }
            }
            catch (Exception)
            {
                var tmp = "tekst";
            }
            if (list.Count == 0)
            {
                Company NoComp = new Company();
                NoComp.Text = "Please go to Server meny and adda server";
                list.Add(NoComp);
            }
            return list;
        }

        public void passID(string id)
        {
            RedirectToPage("SomeOtherPage", new { age = id, });
        }

        public async Task<JsonResult> OnGetFilterAsync(String Adress, String UserID, String Password)
        {
            Server srv = new Server
            {
                Adress = Adress,
                UserID = int.Parse(UserID),
                Password = Password
            };
            List<Company> TMPlist = (await LoadXMLCompanies(await wb.GetXml(srv.Adress, srv.UserID, srv.Password, "3").ConfigureAwait(false), srv.Adress));
            //StoredData.SetServer(srv);
            return new JsonResult(TMPlist);
        }

        private async Task<List<Company>> LoadXMLCompanies(String xml, String srv = "")
        {
            List<Company> rawData = null;
            await Task.Factory.StartNew(delegate
            {
                XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
                XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
                XDocument doc = XDocument.Parse(xml);
                IEnumerable<Company> Companies = from s in doc.Descendants().Where(x => x.Name.LocalName == "properties")

                                                 select new Company
                                                 {
                                                     Id = s.Element(d + "ID").Value,
                                                     Text = s.Element(d + "Name").Value,
                                                     Description = "Salg i dag " + s.Element(d + "SalesAmount").Value,
                                                     Server = srv
                                                 };
                rawData = Companies.ToList();
            });

            StoredData.SetCompanies(rawData);

            return rawData;
        }
    }
}