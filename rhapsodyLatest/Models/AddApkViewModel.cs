using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhapsodyLatest.Models
{
    public class AddApkViewModel
    {
        public int Id { get; set; }
        public string APKName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Developers { get; set; }
        public string PublishDate { get; set; }
        public int Category { get; set; }
        public string OfficialWebsites { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public HttpPostedFileBase APKFile { get; set; }
    }
}