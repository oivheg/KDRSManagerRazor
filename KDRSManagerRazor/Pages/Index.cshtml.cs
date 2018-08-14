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

        //public String GetKey()
        //{
        //    // initialize, with default settings
        //    var storage = new LocalStorage();

        //    //// ... or initialize with a custom configuration
        //    //var config = new LocalStorageConfiguration()
        //    //{
        //    //    // see the section "Configuration" further on
        //    //};

        //    //var storage = new LocalStorage(config);

        //    // store any object, or collection providing only a 'key'
        //    var key = "whatever";
        //    var value = "...";

        //    storage.Store(key, value);
        //    // fetch any object - as object
        //    var skey = storage.Get(key);
        //    return skey.ToString();
        //}

        public void passID(string id)
        {
            RedirectToPage("SomeOtherPage", new { age = id, });
        }

        private async Task<List<Company>> LoadXMLCompanies(String xml)
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
                                                     Description = "Salg den 11.07:  " + s.Element(d + "SalesAmount").Value,
                                                 };
                rawData = Companies.ToList();
            });

            StoredData.SetCompanies(rawData);

            return rawData;
        }
    }
}