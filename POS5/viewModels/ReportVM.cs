using POS5.Models;
using POS5.queryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class ReportVM
    {
        public IEnumerable<Invoice> Invoicelist { get; set; }
        public IEnumerable<PDetail> PDetaillist { get; set; }
     
        public IEnumerable<Table> Tablelist { get; set; }
        public IEnumerable<Pmaster> Pmasterlist { get; set; }
        public IEnumerable<PmasterR> PmasterRlist { get; set; }
        public IEnumerable<PdetailR> PdetailRlist { get; set; }
        public PurchaseReport PurchaseReport { get; set; }
        public decimal final { get; set; }
        public decimal final1 { get; set; }

    }
}