using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class PmasterController : Controller
    {
        private Models.ApplicationDbContext _context;

        public PmasterController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Pmaster
        public ActionResult Index()
        {
            var Pmasterlist = _context.tbl_Pmaster.ToList();
            return View(Pmasterlist);

        }
        public ActionResult New(Pmaster Pmasters)
        {
            var viewModel = new PurchaseVM

            {
                PMaster = Pmasters,
                Itemlist = _context.tbl_Item.ToList(),
                PDetaillist = _context.tbl_PDetail.ToList(),
                Supplierlist = _context.tbl_Supplier.ToList(),


            };
            return View(viewModel);
        }
        public ActionResult Save(PurchaseVM purchase, decimal[] id, string[] Name, decimal[] Qty, decimal[] Price, decimal[] Total)
        {
            if (purchase.PMaster.id == 0)
            {
                purchase.PMaster.id = _context.Database.SqlQuery<decimal>("SELECT ISNULL(MAX(id), 0) + 1 AS id FROM Pmasters").FirstOrDefault();

                for (int i = 0; i < Name.Count(); i++)
                {

                    _context.Database.ExecuteSqlCommand("INSERT INTO PDetails (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + purchase.PMaster.id + ")");

                }
                _context.tbl_Pmaster.Add(purchase.PMaster);

            }
            else
            {
                var Pmasterdb = _context.tbl_Pmaster.Single(c => c.id == purchase.PMaster.id);
                Pmasterdb.date = purchase.PMaster.date;
                Pmasterdb.supplier = purchase.PMaster.supplier;
                Pmasterdb.stock = purchase.PMaster.stock;
                Pmasterdb.grosstotal = purchase.PMaster.grosstotal;
                Pmasterdb.discount = purchase.PMaster.discount;
                Pmasterdb.receivedamount = purchase.PMaster.receivedamount;

                Pmasterdb.balanceamount = purchase.PMaster.balanceamount;
                Pmasterdb.nettotal = purchase.PMaster.nettotal;
                Pmasterdb.grandtotal = purchase.PMaster.grandtotal;
                Pmasterdb.tax = purchase.PMaster.tax;
                Pmasterdb.comment = purchase.PMaster.comment;

                for (int i = 0; i < Name.Count(); i++)
                {
                    decimal mid = purchase.PMaster.id;

                    _context.Database.ExecuteSqlCommand("INSERT INTO PDetails (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + mid + ")");
                }
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


            return RedirectToAction("Index", "Pmaster");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var Pmaster = _context.tbl_Pmaster.SingleOrDefault(c => c.id == Id);
            //if (Pmaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new PurchaseVM
            {
                Supplierlist = _context.tbl_Supplier.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                PDetaillist = _context.Database.SqlQuery<PDetail>("SELECT * FROM PDetails where mid = " + Id),
                PMaster = Pmaster,
               
            };
            return View("New", ViewModel);

            //    return View("New", PMaster);
        }
        public ActionResult Print(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var Pmaster = _context.tbl_Pmaster.SingleOrDefault(c => c.id == Id);
                if (Pmaster == null)
              return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            
            var ViewModel = new PurchaseVM
            {
                Supplierlist = _context.tbl_Supplier.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                PDetaillist = _context.Database.SqlQuery<PDetail>("SELECT * FROM PDetails where mid = " + Id),
                PMaster = Pmaster
            };
            return View("Print", ViewModel);

            //    return View("Print", PMaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Pmaster.SingleOrDefault(c => c.id == Id);
            _context.Database.ExecuteSqlCommand("DELETE FROM PDetails where mid = " + dept.id);
            _context.tbl_Pmaster.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Pmaster");
        }
    }
}