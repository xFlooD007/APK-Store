namespace rhapsodyLatest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class apk
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        [StringLength(50)]
        public string version { get; set; }

        [StringLength(50)]
        public string date { get; set; }

        [StringLength(50)]
        public string developers { get; set; }

        [StringLength(50)]
        public string website { get; set; }

        public float rate { get; set; }
        public float reviews { get; set; }
        public float totalStars { get; set; }

        public int? downloads { get; set; }

        public int? category { get; set; }

        public string img { get; set; }

        public string file_path { get; set; }
    }
}
