using POS5.Models;
using POS5.queryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class LedgerVM
    {
        public IEnumerable<Item> Itemlist { get; set; }
        public IEnumerable<ItemLedger> ItemLedgerList { get; set; }
        public parameter parameter { get; set; }
        public ItemLedger ItemLedger { get; set; }

       


    }
}