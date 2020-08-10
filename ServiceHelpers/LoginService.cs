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
    public class LoginService
    {
       public static readonly string BaseUri = ConfigurationManager.AppSettings["LoginManagerBase"];
        public async static Task<LoginModel> Login(LoginModel login)
        {
            LoginModel returnmessage = new LoginModel();          

            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("Login/AuthenticateUser", login).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoginModel>();
                    returnmessage = result;
                }
            }
            return returnmessage;
        }
        public async static Task<List<ListItems>> UserRoleList(string UserName)
        {
            var returnmessage = new List<ListItems>();            

            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("Login/{0}/UserRole", UserName)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ListItems>>();
                    foreach (var p in result)
                    {
                        returnmessage.Add(new ListItems() { ID = p.ID, Value = p.Value });
                    }
                }
            }
            return returnmessage;
        }
    }
}