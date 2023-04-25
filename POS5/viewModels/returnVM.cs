using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class returnVM
    {
        public IEnumerable<Item> Itemlist { get; set; }
        public IEnumerable<SaleDetailReturn> SaleDetailRlist { get; set; }
        public IEnumerable<Customer> Customerlist { get; set; }

        public Item Item { get; set; }
        public SaleMasterReturn SaleMasterR { get; set; }

        public SaleDetailReturn SaledetailR { get; set; }

    }
}