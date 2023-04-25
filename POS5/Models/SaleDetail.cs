using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.Models
{
    public class SaleDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public decimal mid { get; set; }
        public decimal itemid { get; set; }
        public string itemname { get; set; }
        public decimal saleprice { get; set; }
        public decimal quantity { get; set; }
        public decimal total { get; set; }
        

    }
}
