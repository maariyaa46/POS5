using POS5.Models;
using POS5.queryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class newSaleVM
    {
        public IEnumerable<Item> Itemlist { get; set; }
        public IEnumerable<SaleDetail> SaleDetaillist { get; set; }
        public IEnumerable<Customer> Customerlist { get; set; }

        public Item Item { get; set; }
        public SalesMaster SaleMaster { get; set; }

        public SaleDetail Saledetail { get; set; }
        

    }
}