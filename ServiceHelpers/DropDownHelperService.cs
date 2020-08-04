using DocsOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DocsOnline.ServiceHelpers
{
    public class DropDownHelperService
    {
        private static readonly string CostModelAPIURL = ConfigurationManager.AppSettings["AMSBaseURL"];
        public static async Task<List<ListItems>> ProjectList()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("TechProjects/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
    }
}