using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.queryModel
{
    public class PurchaseReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public string tablename { get; set; }
        public string invoicename { get; set; }
        public string view { get; set; }
        public DateTime dateFrom { get; set; }
   
        public DateTime dateTo { get; set; }


    }
}
