using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class PmasterRController : Controller
    {
        private Models.ApplicationDbContext _context;

        public PmasterRController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: PmasterR
        public ActionResult Index()
        {
            var PmasterRlist = _context.tbl_PmasterR.ToList();
            return View(PmasterRlist);

        }
        public ActionResult New(PmasterR PmasterR)
        {
            var viewModel = new PreturnVM

            {
                PMasterR = PmasterR,
                Itemlist = _context.tbl_Item.ToList(),
                PDetailRlist = _context.tbl_PdetailR.ToList(),
                Supplierlist = _context.tbl_Supplier.ToList(),


            };
            return View(viewModel);
        }
        public ActionResult Save(PreturnVM preturn, decimal[] id, string[] Name, decimal[] Qty, decimal[] Price, decimal[] Total)
        {
            if (preturn.PMasterR.id == 0)
            {
                preturn.PMasterR.id = _context.Database.SqlQuery<decimal>("SELECT ISNULL(MAX(id), 0) + 1 AS id FROM PmasterRs").FirstOrDefault();

                for (int i = 0; i < Name.Count(); i++)
                {

                    _context.Database.ExecuteSqlCommand("INSERT INTO PdetailRs (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + preturn.PMasterR.id + ")");

                }
                _context.tbl_PmasterR.Add(preturn.PMasterR);

            }
            else
            {
                var PmasterRdb = _context.tbl_PmasterR.Single(c => c.id == preturn.PMasterR.id);
                PmasterRdb.date = preturn.PMasterR.date;
                PmasterRdb.supplier = preturn.PMasterR.supplier;
                PmasterRdb.stock = preturn.PMasterR.stock;
                PmasterRdb.grosstotal = preturn.PMasterR.grosstotal;
                PmasterRdb.discount = preturn.PMasterR.discount;
                PmasterRdb.receivedamount = preturn.PMasterR.receivedamount;

                PmasterRdb.balanceamount = preturn.PMasterR.balanceamount;
                PmasterRdb.nettotal = preturn.PMasterR.nettotal;
                PmasterRdb.grandtotal = preturn.PMasterR.grandtotal;
                PmasterRdb.tax = preturn.PMasterR.tax;
                PmasterRdb.comment = preturn.PMasterR.comment;
                for (int i = 0; i < Name.Count(); i++)
                {
                    decimal mid = preturn.PMasterR.id;

                    _context.Database.ExecuteSqlCommand("INSERT INTO PdetailRs (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + mid + ")");
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


            return RedirectToAction("Index", "PmasterR");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var PmasterR = _context.tbl_PmasterR.SingleOrDefault(c => c.id == Id);
            //if (PmasterR == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new PreturnVM
            {
                Supplierlist = _context.tbl_Supplier.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                PDetailRlist = _context.Database.SqlQuery<PdetailR>("SELECT * FROM PdetailRs where mid = " + Id),
                PMasterR = PmasterR
            };
            return View("New", ViewModel);

            //    return View("New", PmasterR);
        }
        public ActionResult Print(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var PmasterR = _context.tbl_PmasterR.SingleOrDefault(c => c.id == Id);
            //if (PmasterR == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new PreturnVM
            {
                Supplierlist = _context.tbl_Supplier.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                PDetailRlist = _context.Database.SqlQuery<PdetailR>("SELECT * FROM PdetailRs  where mid = " + Id),
                PMasterR = PmasterR
            };
            return View("Print", ViewModel);

            //    return View("New", PmasterR);
        }

        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_PmasterR.SingleOrDefault(c => c.id == Id);
            _context.Database.ExecuteSqlCommand("DELETE FROM PdetailRs  where mid = " + dept.id);
            _context.tbl_PmasterR.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "PmasterR");
        }
    }
}