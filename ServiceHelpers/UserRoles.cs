using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.ServiceHelpers
{
    public class UserRoles
    {
        public static bool UserEditCompleteBookings()
        {
            string UserName = System.Web.HttpContext.Current.Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator"
            || r.Value == "Accounts" || r.Value == "WarehouseManager" || r.Value == "ProjectmanagerAdmin").FirstOrDefault() != null ? true : false;
        }
        public static bool UserCanViewResourceDetails()
        {
            if (System.Web.HttpContext.Current.Session["Username"] !=null)
            {
                return true;
            }
            return false;
        }
    }
}
