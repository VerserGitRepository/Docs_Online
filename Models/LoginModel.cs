using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}