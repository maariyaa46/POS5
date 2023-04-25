using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class PurchaseVM
    {
        public IEnumerable<Item> Itemlist { get; set; }
        public IEnumerable<PDetail> PDetaillist { get; set; }
        public IEnumerable<Supplier> Supplierlist { get; set; }

        public Item Item { get; set; }
        public  Pmaster PMaster { get; set; }

        public PDetail Pdetail { get; set; }

    }
}