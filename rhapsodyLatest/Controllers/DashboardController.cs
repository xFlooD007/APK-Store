using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using rhapsodyLatest.Auth;
using rhapsodyLatest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace rhapsodyLatest.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            ViewData["AllUsers"] = userManager.Users;

            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                ViewData["NumOfAllApks"] = db.apks.Count();
            }
            return View();
        }

        [Authorize(Roles = "Admin,Owner")]
        public ActionResult ContentManagement()
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                ViewData["AllApk"] = db.apks.OrderByDescending(x => x.downloads).ToList();
            }
            return View();
        }

        [Authorize(Roles = "Admin,Owner")]
        public ActionResult AddApk()
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1",new List<string>() { "Choose Category" }, "Choose Category");
            }
            return View();
        }
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult EditAPK(int id)
        {
            apk apkInstance;
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                apkInstance = db.apks.ToList().Find(a => a.id == id);
                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1", new List<string>() { "Choose Category" }, "Choose Category");
            }

            return View(apkInstance);
        }
        [Authorize(Roles = "Admin,Owner")]
        [HttpPost]
        public ActionResult AddApk(AddApkViewModel model)
        {
            using(EFCodeFirstModel db = new EFCodeFirstModel())
            {
                apk uploadedAPK = new apk();
                int id = db.apks.Count() + 1;
                uploadedAPK.category = model.Category;
                uploadedAPK.date = model.PublishDate;
                uploadedAPK.description = model.Description;
                uploadedAPK.developers = model.Developers;
                uploadedAPK.downloads = 0;
                uploadedAPK.file_path = $"Applications/{id}/{model.APKFile.FileName}";
                uploadedAPK.img = $"Applications/{id}/{model.Image.FileName}";
                uploadedAPK.rate = 0;
                uploadedAPK.version = model.Version;
                uploadedAPK.website = model.OfficialWebsites;
                uploadedAPK.name = model.APKName;

                string sitePath = ConfigurationManager.AppSettings["SitePath"].ToString();
                if (!Directory.Exists(sitePath + "Applications/" + id))
                    Directory.CreateDirectory(sitePath + "Applications/" + id);

                model.Image.SaveAs(sitePath + uploadedAPK.img);
                model.APKFile.SaveAs(sitePath + uploadedAPK.file_path);

                db.apks.Add(uploadedAPK);
                db.SaveChanges();

                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1", new List<string>() { "Choose Category" }, "Choose Category");
            }
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        [Authorize(Roles = "Admin,Owner")]
        public bool ChangeRole(string UserID,string RoleName)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            userManager.RemoveFromRoles(UserID, userManager.GetRoles(UserID).ToArray());
            userManager.AddToRole(UserID, RoleName);
            return true;
        }

        [Authorize(Roles = "Admin,Owner")] 
        public bool RemoveAPK(int ApkID)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                db.apks.Remove(db.apks.ToList().Find(a => a.id == ApkID));
                db.SaveChanges();
            }

            return true;
        }
    }
}