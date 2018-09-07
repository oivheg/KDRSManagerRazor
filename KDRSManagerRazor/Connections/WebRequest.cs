using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KDRSManagerRazor.Connections
{
    public class WebRequest
    {
        public async Task<String> GetXml(String _srv, int _Username, String _pw, String _rt, String From = "", String To = "")
        {
            String response = null;
            //Check network status
            DateTime DT = DateTime.Today;

            if (!From.Equals(""))
            {
                DT = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            String srv = _srv;
            String RT = _rt;
            int YR = DT.Year;
            int M = DT.Month;
            int D = DT.Day;
            //D = 22;
            //M = 8;
            int username = _Username;
            String pw = _pw;
            Uri geturi = new Uri("http://" + srv + ":8008/KDRPhoneService.svc/GetSalesReport?reportType=" + RT + "&yr=" + YR + "&m=" + M + "&d=" + D + "&username=%27" + username + "%27&password=%27" + pw + "%27"); //replace your xml url
            HttpClient client = new HttpClient();
            HttpResponseMessage responseGet = await client.GetAsync(geturi);
            response = await responseGet.Content.ReadAsStringAsync();//Getting response

            return response;
        }
    }
}