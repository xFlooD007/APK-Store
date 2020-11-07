namespace rhapsodyLatest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public int id { get; set; }

        [Column("category")]
        [Required]
        [StringLength(50)]
        public string category1 { get; set; }
    }
}
