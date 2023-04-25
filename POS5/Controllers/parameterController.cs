using POS5.Models;
using POS5.queryModel;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class parameterController : Controller
    {
        private Models.ApplicationDbContext _context;

        public parameterController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: parameter
        
        public ActionResult New(parameter parameter)
        {
            var viewModel = new LedgerVM

            {
                Itemlist = _context.tbl_Item.ToList(),
               
                parameter = parameter
            };
            return View(viewModel);
        }

        
    }
}
