using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using rhapsodyLatest.Auth;
using rhapsodyLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhapsodyLatest.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser appuser = new AppUser();
                appuser.UserName = model.Username;
                appuser.Email = model.Email;
                
                var createUserResult = await userManager.CreateAsync(appuser,model.Password);
                

                if (createUserResult.Succeeded)
                {
                    var addToRoleResult = userManager.AddToRole(appuser.Id, "Member");

                    var ident = userManager.CreateIdentity(appuser,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created.
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);

                    return Redirect(Url.Action("Index", "Home"));
                }
                else
                {
                    ViewData["Errors"] = createUserResult.Errors.FirstOrDefault();
                    return View("~/Views/Register/Index.cshtml");
                }

            }
            return View("~/Views/Register/Index.cshtml");
        }
    }
}