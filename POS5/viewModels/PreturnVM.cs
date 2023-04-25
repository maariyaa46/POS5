using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class PreturnVM
    {
        public IEnumerable<Item> Itemlist { get; set; }
        public IEnumerable<PdetailR> PDetailRlist { get; set; }
        public IEnumerable<Supplier> Supplierlist { get; set; }

        public Item Item { get; set; }
        public PmasterR PMasterR { get; set; }

        public PdetailR PdetailR { get; set; }

    }
}