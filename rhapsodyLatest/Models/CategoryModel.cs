using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhapsodyLatest.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<apk> APKs { get; set; }
    }
}