using rhapsodyLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhapsodyLatest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            using(EFCodeFirstModel db = new EFCodeFirstModel())
            {
                foreach(var cat in db.Categories.ToList())
                {
                    CategoryModel newCat = new CategoryModel();
                    newCat.ID = cat.id;
                    newCat.Name = cat.category1;
                    newCat.APKs = db.apks.ToList().Where(x => x.category == cat.id).ToList();
                    categories.Add(newCat);
                }

                this.ViewData["Categories"] = categories;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}