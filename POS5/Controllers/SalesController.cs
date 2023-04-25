using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SalesController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SalesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Sales
        public ActionResult Index()
        {
            var Saleslist = _context.tbl_Sales.ToList();
            return View(Saleslist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Sales Sales)
        {

            if (Sales.id == 0)
            {

                _context.tbl_Sales.Add(Sales);
            }
            else
            {
                var Salesdb = _context.tbl_Sales.Single(c => c.id == Sales.id);
                Salesdb.name = Sales.name;
                Salesdb.total = Sales.total;
                
                Salesdb.price = Sales.price;
                Salesdb.date = Sales.price;
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


            return RedirectToAction("Index", "Sales");
        }
        public ActionResult Edit(int Id)
        {
            var Sales = _context.tbl_Sales.SingleOrDefault(c => c.id == Id);
            if (Sales == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Sales);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Sales.SingleOrDefault(c => c.id == Id);
            _context.tbl_Sales.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Sales");
        }
    }
}