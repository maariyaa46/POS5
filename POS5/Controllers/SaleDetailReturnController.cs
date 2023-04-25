using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SaleDetailReturnController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SaleDetailReturnController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: SaleDetailReturn
        public ActionResult Index()
        {
            var SaleDetailReturnlist = _context.tbl_SaleDetailReturn.ToList();
            return View(SaleDetailReturnlist);

        }
        public ActionResult New(SaleDetailReturn SaledetailR)
        {
            var viewModel = new returnVM

            {
                SaledetailR = SaledetailR,
                Itemlist = _context.tbl_Item.ToList(),
            };
            return View();
        }
        public ActionResult Save(returnVM returnvm)
        {

            if (returnvm.SaledetailR.id == 0)
            {

                _context.tbl_SaleDetailReturn.Add(returnvm.SaledetailR);
            }
            else
            {
                var SaleDetailReturndb = _context.tbl_SaleDetailReturn.Single(c => c.id == returnvm.SaledetailR.id);
                SaleDetailReturndb.mid = returnvm.SaledetailR.mid;
                SaleDetailReturndb.itemid = returnvm.SaledetailR.itemid;
                SaleDetailReturndb.itemname = returnvm.SaledetailR.itemname;
                SaleDetailReturndb.saleprice = returnvm.SaledetailR.saleprice;
                SaleDetailReturndb.quantity = returnvm.SaledetailR.quantity;
                SaleDetailReturndb.total = returnvm.SaledetailR.total;

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


            return RedirectToAction("New", "SaleDetailReturn");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var SalesdetailR = _context.tbl_SaleDetailReturn.SingleOrDefault(c => c.id == Id);
            //if (SalesMaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new returnVM
            {
                Itemlist = _context.tbl_Item.ToList(),

                SaledetailR = SalesdetailR
            };
            return View("New", ViewModel);

            //    return View("New", SalesMaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_SaleDetailReturn.SingleOrDefault(c => c.id == Id);
            _context.tbl_SaleDetailReturn.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "SaleDetailReturn");
        }
    }
}