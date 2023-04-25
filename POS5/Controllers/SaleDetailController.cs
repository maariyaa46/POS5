using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SaleDetailController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SaleDetailController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: SaleDetail
        public ActionResult Index()
        {
            var SaleDetaillist = _context.tbl_SaleDetail.ToList();
            return View(SaleDetaillist);

        }
        public ActionResult New(SaleDetail Saledetail)
        {
            var viewModel = new newSaleVM

            {
                Saledetail= Saledetail,
                Itemlist = _context.tbl_Item.ToList(),
            };
            return View(viewModel);
        }

        public ActionResult Save(newSaleVM SaleVM)
        {

            if (SaleVM.Saledetail.id == 0)
            {

                _context.tbl_SaleDetail.Add(SaleVM.Saledetail);
            }
            else
            {
                var SaleDetaildb = _context.tbl_SaleDetail.Single(c => c.id == SaleVM.Saledetail.id);
                SaleDetaildb.mid = SaleVM.Saledetail.mid;
                SaleDetaildb.itemid = SaleVM.Saledetail.itemid;
                SaleDetaildb.itemname = SaleVM.Saledetail.itemname;
                SaleDetaildb.saleprice = SaleVM.Saledetail.saleprice;
                SaleDetaildb.quantity = SaleVM.Saledetail.quantity;
                SaleDetaildb.total = SaleVM.Saledetail.total;
                
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


            return RedirectToAction("Index", "SaleDetail");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var Salesdetail = _context.tbl_SaleDetail.SingleOrDefault(c => c.id == Id);
            //if (SalesMaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new newSaleVM
            {
                Itemlist = _context.tbl_Item.ToList(),

                Saledetail = Salesdetail
            };
            return View("New", ViewModel);

            //    return View("New", SalesMaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_SaleDetail.SingleOrDefault(c => c.id == Id);
            _context.tbl_SaleDetail.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "SaleDetail");
        }
    }
}