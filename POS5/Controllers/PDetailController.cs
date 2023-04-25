using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class PDetailController : Controller
    {
        private Models.ApplicationDbContext _context;

        public PDetailController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: pdetail
        public ActionResult Index()
        {
            var PDetaillist = _context.tbl_PDetail.ToList();
            return View(PDetaillist);

        }
        public ActionResult New(PDetail Pdetail)
        {
            var viewModel = new PurchaseVM

            {
                Pdetail = Pdetail,
                Itemlist = _context.tbl_Item.ToList(),
            };
            return View(viewModel);
        }

        public ActionResult Save(PurchaseVM Purchase)
        {

            if (Purchase.Pdetail.id == 0)
            {

                _context.tbl_PDetail.Add(Purchase.Pdetail);
            }
            else
            {
                var PDetaildb = _context.tbl_PDetail.Single(c => c.id == Purchase.Pdetail.id);
                PDetaildb.mid = Purchase.Pdetail.mid;
                PDetaildb.itemid = Purchase.Pdetail.itemid;
                PDetaildb.itemname = Purchase.Pdetail.itemname;
                PDetaildb.saleprice = Purchase.Pdetail.saleprice;
              
                PDetaildb.quantity = Purchase.Pdetail.quantity;
                PDetaildb.total = Purchase.Pdetail.total;
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


            return RedirectToAction("Index", "PDetail");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var Pdetails = _context.tbl_PDetail.SingleOrDefault(c => c.id == Id);
            //if (Pmaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new PurchaseVM
            {
                Itemlist = _context.tbl_Item.ToList(),

                Pdetail = Pdetails
            };
            return View("New", ViewModel);

            //    return View("New", Pmaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_PDetail.SingleOrDefault(c => c.id == Id);
            _context.tbl_PDetail.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "PDetail");
        }
    }
}