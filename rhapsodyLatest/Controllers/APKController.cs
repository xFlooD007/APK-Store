using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhapsodyLatest.Controllers
{
    public class APKController : Controller
    {
        // GET: APK
        public ActionResult Index(int id)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                string sitePath = ConfigurationManager.AppSettings["SitePath"].ToString();
                var apk = db.apks.ToList().Find(a => a.id == id);
                ViewData["apk"] = apk;

                FileInfo fi = new FileInfo(sitePath + apk.file_path);

                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = fi.Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }

                ViewData["fileSize"] = String.Format("{0:0.##} {1}", len, sizes[order]);
            }
            return View();
        }

        [Authorize]
        public void rate(int id,float stars)
        {
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                var apk = db.apks.ToList().Find(a => a.id == id);
                apk.reviews++;
                apk.totalStars += stars;
                apk.rate = apk.totalStars / apk.reviews;

                db.SaveChanges();
            }
        }

        [Authorize]
        public FileResult Download(int id)
        {
            apk apkFile;
            using (EFCodeFirstModel db = new EFCodeFirstModel())
            {
                apkFile = db.apks.ToList().Find(a => a.id == id);
                db.apks.ToList().Find(a => a.id == id).downloads++;
                db.SaveChanges();
            }
            string sitePath = ConfigurationManager.AppSettings["SitePath"].ToString();

            byte[] fileBytes = System.IO.File.ReadAllBytes(sitePath + apkFile.file_path);
            string contentType = MimeMapping.GetMimeMapping(sitePath + apkFile.file_path);
            FileInfo fi = new FileInfo(sitePath + apkFile.file_path);
            return File(fileBytes, contentType, fi.Name);
        }
    }
}