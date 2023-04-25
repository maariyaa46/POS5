using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class PdetailRController : Controller
    {
        private Models.ApplicationDbContext _context;

        public PdetailRController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: PdetailR
        public ActionResult Index()
        {
            var PdetailRlist = _context.tbl_PdetailR.ToList();
            return View(PdetailRlist);

        }
        public ActionResult New(PdetailR PdetailR)
        {
            var viewModel = new PreturnVM

            {
                PdetailR = PdetailR,
                Itemlist = _context.tbl_Item.ToList(),
            };
            return View(viewModel);
        }

        public ActionResult Save(PreturnVM PreturnVM)
        {

            if (PreturnVM.PdetailR.id == 0)
            {

                _context.tbl_PdetailR.Add(PreturnVM.PdetailR);
            }
            else
            {
                var PdetailRdb = _context.tbl_PdetailR.Single(c => c.id == PreturnVM.PdetailR.id);
                PdetailRdb.mid = PreturnVM.PdetailR.mid;
                PdetailRdb.itemid = PreturnVM.PdetailR.itemid;
                PdetailRdb.itemname = PreturnVM.PdetailR.itemname;
                PdetailRdb.saleprice = PreturnVM.PdetailR.saleprice;

                PdetailRdb.quantity = PreturnVM.PdetailR.quantity;
                PdetailRdb.total = PreturnVM.PdetailR.total;
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


            return RedirectToAction("Index", "PdetailR");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var PdetailRs = _context.tbl_PdetailR.SingleOrDefault(c => c.id == Id);
            //if (Pmaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new PreturnVM
            {
                Itemlist = _context.tbl_Item.ToList(),

                PdetailR = PdetailRs
            };
            return View("New", ViewModel);

            //    return View("New", Pmaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_PdetailR.SingleOrDefault(c => c.id == Id);
            _context.tbl_PdetailR.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "PdetailR");
        }
    }
}