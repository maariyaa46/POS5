using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class DesignationController : Controller
    {
        private Models.ApplicationDbContext _context;

        public DesignationController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Designation
        public ActionResult Index()
        {
            var Designationlist = _context.tbl_Designation.ToList();
            return View(Designationlist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Designation Designation)
        {

            if (Designation.id == 0)
            {

                _context.tbl_Designation.Add(Designation);
            }
            else
            {
                var Designationdb = _context.tbl_Designation.Single(c => c.id == Designation.id);
                Designationdb.name = Designation.name;
                Designationdb.contactno = Designation.contactno;
                Designationdb.status = Designation.status;
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


            return RedirectToAction("New", "Designation");
        }
        public ActionResult Edit(int Id)
        {
            var Designation = _context.tbl_Designation.SingleOrDefault(c => c.id == Id);
            if (Designation == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Designation);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Designation.SingleOrDefault(c => c.id == Id);
            _context.tbl_Designation.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Designation");
        }
    }
}