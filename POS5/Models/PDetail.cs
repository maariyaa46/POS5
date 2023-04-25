using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POS5.Models
{
    public class PDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public decimal mid { get; set; }
        public decimal itemid { get; set; }
        public string itemname { get; set; }
        public decimal saleprice { get; set; }
        //public decimal purchaseprice { get; set; }
        public decimal quantity { get; set; }
        public decimal total { get; set; }
    }
}