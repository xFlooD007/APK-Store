using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using rhapsodyLatest.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace rhapsodyLatest
{
    public static class Extentions
    {
        public static string Role(this IPrincipal user)
        {
            if (user.IsInRole("Owner"))
                return "Owner";
            else if (user.IsInRole("Admin"))
                return "Admin";
            else
                return "Member";
        }

        public static string Role(this AppUser user)
        {
            var um = new UserManager<AppUser>(new UserStore<AppUser>(new EFCodeFirstModel()));
            if (um.GetRoles(user.Id).Count > 0)
                return um.GetRoles(user.Id)[0];
            else
                return "Member";
        }
    }
}