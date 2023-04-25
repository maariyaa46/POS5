using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class StocksController : Controller
    {
        private Models.ApplicationDbContext _context;

        public StocksController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Stocks
        public ActionResult Index()
        {
            var Stockslist = _context.tbl_Stocks.ToList();
            return View(Stockslist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Stocks Stocks)
        {

            if (Stocks.id == 0)
            {

                _context.tbl_Stocks.Add(Stocks);
            }
            else
            {
                var Stocksdb = _context.tbl_Stocks.Single(c => c.id == Stocks.id);
                Stocksdb.name = Stocks.name;
                
                Stocksdb.total = Stocks.total;
                Stocksdb.price = Stocks.price;
                Stocksdb.sold = Stocks.price;

            }

            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


            return RedirectToAction("Index", "Stocks");
        }
        public ActionResult Edit(int Id)
        {
            var Stocks = _context.tbl_Stocks.SingleOrDefault(c => c.id == Id);
            if (Stocks == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Stocks);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Stocks.SingleOrDefault(c => c.id == Id);
            _context.tbl_Stocks.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Stocks");
        }
    }
}