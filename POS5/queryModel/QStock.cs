using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.queryModel
{
    public class QStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public string name { get; set; }
        public decimal stocklevel { get; set; }

        public decimal PurchasedetailIN { get; set; }
        public decimal SalesReturnIN { get; set; }
        public decimal SalesOUT { get; set; }
       public decimal PurchaseDetailOUT { get; set;}
       public decimal Total { get; set; }
    }
}