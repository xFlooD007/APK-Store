using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhapsodyLatest.Controllers
{
    public class SearchResultsController : Controller
    {
        public ActionResult Index(string search)
        {
            List<apk> apks = new List<apk>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                using (EFCodeFirstModel db = new EFCodeFirstModel())
                {
                    apks = db.apks.ToList().Where(x => x.name.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1).ToList();
                }
            }

            return View(apks);
        }
    }
}