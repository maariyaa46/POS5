using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS5.Models
{
    public class SaleMasterReturn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }
        public DateTime date { get; set; }
        public string customerName { get; set; }

        public decimal grosstotal { get; set; }
        public decimal discount { get; set; }
        public decimal receivedamount { get; set; }
        public string comment { get; set; }
        public decimal balanceamount { get; set; }
        public decimal grandtotal { get; set; }
        public decimal nettotal { get; set; }
        public decimal tax { get; set; }

    }
}
