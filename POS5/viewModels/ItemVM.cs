using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS5.viewModels
{
    public class ItemVM
    {
        public IEnumerable<Category> Categorylist { get; set; }
        public IEnumerable<Brand> Brandlist { get; set; }
        public Item Item { get; set; }
        public Category Category{ get; set; }


    }
}