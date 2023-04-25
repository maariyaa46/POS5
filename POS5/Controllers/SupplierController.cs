using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SupplierController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SupplierController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Supplier
        public ActionResult Index()
        {
            var Supplierlist = _context.tbl_Supplier.ToList();
            return View(Supplierlist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Supplier Supplier)
        {

            if (Supplier.id == 0)
            {

                _context.tbl_Supplier.Add(Supplier);
            }
            else
            {
                var Supplierdb = _context.tbl_Supplier.Single(c => c.id == Supplier.id);
                Supplierdb.name = Supplier.name;

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


            return RedirectToAction("New", "Supplier");
        }
        public ActionResult Edit(int Id)
        {
            var Supplier = _context.tbl_Supplier.SingleOrDefault(c => c.id == Id);
            if (Supplier == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Supplier);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Supplier.SingleOrDefault(c => c.id == Id);
            _context.tbl_Supplier.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Supplier");
        }
    }
}