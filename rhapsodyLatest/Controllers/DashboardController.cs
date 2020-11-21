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
                ViewData["categories"] = db.Categories.ToList();
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
        [Authorize]
        public ActionResult SubmitApk()
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1", new List<string>() { "Choose Category" }, "Choose Category");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitApk(AddApkViewModel model)
        {
            return View();
        }
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult EditAPK(int id)
        {
            apk apkInstance;
            AddApkViewModel model = new AddApkViewModel();
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                apkInstance = db.apks.ToList().Find(a => a.id == id);
                model.APKName = apkInstance.name;
                model.Category = (int)apkInstance.category;
                model.Description = apkInstance.description;
                model.Developers = apkInstance.developers;
                model.OfficialWebsites = apkInstance.website;
                model.PublishDate = apkInstance.date;
                model.Version = apkInstance.version;
                model.Id = apkInstance.id;

                ViewData["ImagePath"] = apkInstance.img;
                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1", new List<string>() { "Choose Category" }, "Choose Category");
            }

            return View(model);
        }
        [Authorize(Roles = "Admin,Owner")]
        [HttpPost]
        public ActionResult EditApk(AddApkViewModel model)
        {

            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                apk apkInstance = db.apks.ToList().Find(a => a.id == model.Id);
                apkInstance.name = model.APKName;
                apkInstance.category = model.Category;
                apkInstance.date = model.PublishDate;
                apkInstance.description = model.Description;
                apkInstance.developers = model.Developers;
                apkInstance.version = model.Version;
                apkInstance.website = model.OfficialWebsites;

                string appPath = ConfigurationManager.AppSettings["SitePath"].ToString()+"Applications/"+ model.Id+"/";
                if (model.Image != null)
                {
                    model.Image.SaveAs(appPath + model.Image.FileName);
                    apkInstance.img = $"Applications/{model.Id}/{model.Image.FileName}";
                }
                if (model.APKFile != null)
                {
                    model.Image.SaveAs(appPath + model.APKFile.FileName);
                    apkInstance.file_path = $"Applications/{model.Id}/{model.APKFile.FileName}";
                }
                db.SaveChanges();

                ViewData["ImagePath"] = apkInstance.img;
                ViewData["Categories"] = db.Categories.ToList();
                ViewData["SelectListItems"] = new SelectList(db.Categories.ToList(), "id", "category1", new List<string>() { "Choose Category" }, "Choose Category");
            }

            return View(model);
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
        public string ChangeRole(string UserID,string RoleName)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            userManager.RemoveFromRoles(UserID, userManager.GetRoles(UserID).ToArray());
            userManager.AddToRole(UserID, RoleName);
            return "Role changed Successfully!";
        }

        [Authorize(Roles = "Admin,Owner")] 
        public string RemoveAPK(int ApkID)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                db.apks.Remove(db.apks.ToList().Find(a => a.id == ApkID));
                db.SaveChanges();
            }

            return "Success!";
        }

        [Authorize(Roles = "Admin,Owner")]
        public string EditCategoryName(int catID,string newName)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                if (db.Categories.Any(c => c.category1.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                {
                    Response.StatusCode = 201;
                    return "There is another category with the same name, Please choose different name and try again..";
                }

                db.Categories.Find(catID).category1 = newName;
                db.SaveChanges();
            }

            return "Category name was changed successfully!";
        }

        [Authorize(Roles = "Admin,Owner")]
        public string CreateNewCategory(string name)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                if (db.Categories.Any(c => c.category1.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    Response.StatusCode = 201;
                    return "There is another category with the same name, Please choose different name and try again..";
                }

                db.Categories.Add(new Category() { category1 = name });
                db.SaveChanges();
            }

            return "Success!!";
        }

        [Authorize(Roles = "Admin,Owner")]
        public string RemoveCategory(int id)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                var gotAPKS = db.apks.Any(a => a.category == id);
                if (gotAPKS)
                {
                    Response.StatusCode = 201;
                    return "Category already contains APKs, Please remove them first.";
                }

                db.Categories.Remove(db.Categories.Find(id));
                db.SaveChanges();
            }

            return "Success!!";
        }
    }
}