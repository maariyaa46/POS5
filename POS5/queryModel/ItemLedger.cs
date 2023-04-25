using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.queryModel
{
    public class ItemLedger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public decimal pur { get; set; }
        public decimal preturnid { get; set; }
        public decimal sreturnid { get; set; }

        public string itemname { get; set; }
        public DateTime dateF { get; set; }
        public decimal SaleIN { get; set; }
        public decimal PurchaseIN { get; set; }
        public decimal SaleReturnOUT { get; set; }
        public decimal PurchaseReturnOUT { get; set; }
        public decimal Balance { get; set; }
        
    }
}