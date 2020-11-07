using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhapsodyLatest.Models
{
    public class APKModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public string date { get; set; }
        public string developers { get; set; }
        public string website { get; set; }
        public string rate { get; set; }
        public int downloads { get; set; }
        public int category { get; set; }
        public string img { get; set; }
        public string file_path { get; set; }
    }
}