using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace POS5.Models
{
    public partial class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public decimal brandid { get; set; }

        //public string img { get; set; }

        [NotMapped]
        public HttpPostedFileBase imgFile { get; set; }


    }
}
