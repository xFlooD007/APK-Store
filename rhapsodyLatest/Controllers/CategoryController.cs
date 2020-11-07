using rhapsodyLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhapsodyLatest.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int id)
        {
            CategoryModel catMod = new CategoryModel();
            using(EFCodeFirstModel db = new EFCodeFirstModel())
            {
                var cat = db.Categories.Where(c => c.id == id).ToList()[0];
                catMod.ID = cat.id;
                catMod.Name = cat.category1;
                catMod.APKs = db.apks.Where(a => a.category == cat.id).ToList();
            }

            return View(catMod);
        }
    }
}