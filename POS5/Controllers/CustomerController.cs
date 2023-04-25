using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class CustomerController : Controller
    {
        private Models.ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Customer
        public ActionResult Index()
        {
            var Customerlist = _context.tbl_Customer.ToList();
            return View(Customerlist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Customer Customer)
        {

            if (Customer.id == 0)
            {

                _context.tbl_Customer.Add(Customer);
            }
            else
            {
                var Customerdb = _context.tbl_Customer.Single(c => c.id == Customer.id);
                Customerdb.name = Customer.name;

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


            return RedirectToAction("New", "Customer");
        }
        public ActionResult Edit(int Id)
        {
            var Customer = _context.tbl_Customer.SingleOrDefault(c => c.id == Id);
            if (Customer == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Customer);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Customer.SingleOrDefault(c => c.id == Id);
            _context.tbl_Customer.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
}