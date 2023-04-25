using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.Models
{
    public class Stocks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public string name { get; set; }
        public decimal total { get; set; }
        public decimal sold { get; set; }
        public decimal price { get; set; }
        
    }
}
