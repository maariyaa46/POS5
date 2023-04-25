using POS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class BrandController : Controller
    {
        private Models.ApplicationDbContext _context;

        public BrandController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Brand
        public ActionResult Index()
        {
            var Brandlist = _context.tbl_Brand.ToList();
            return View(Brandlist);

        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(Brand Brand)
        {

            if (Brand.id == 0)
            {

                _context.tbl_Brand.Add(Brand);
            }
            else
            {
                var Branddb = _context.tbl_Brand.Single(c => c.id == Brand.id);
                Branddb.name = Brand.name;
                
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


            return RedirectToAction("New", "Brand");
        }
        public ActionResult Edit(int Id)
        {
            var Brand = _context.tbl_Brand.SingleOrDefault(c => c.id == Id);
            if (Brand == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("New", Brand);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Brand.SingleOrDefault(c => c.id == Id);
            _context.tbl_Brand.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
    }
}