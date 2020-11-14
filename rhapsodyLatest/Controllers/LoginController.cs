using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class LoginController : Controller
    {
        // GET: Login
        //public ActionResult Index()
        //{

        //    var authManager = HttpContext.GetOwinContext().Authentication;

        //    if (authManager.User.Identity.IsAuthenticated)
        //    {
        //        return Redirect(Url.Action("Index", "dashboard"));
        //    }

        //    return View();
        //}

        public ActionResult Index(string returnUrl)
        {
            var authManager = HttpContext.GetOwinContext().Authentication;

            if (authManager.User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("Index", "dashboard"));
            }

            var model = new LoginViewModel() { ReturnUrl = returnUrl };
            return View(model);
        }



        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser user = userManager.Find(login.UserName, login.Password);

                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created.
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(login.ReturnUrl ?? Url.Action("Index", "dashboard"));
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View("~/Views/Login/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Login/Index.cshtml");
        }
        public ActionResult CreateUser()
        {
            //userManager.Create(new AppUser() { UserName = login.UserName }, login.Password);
            return View();
        }
    }
}