using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SaleMasterReturnController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SaleMasterReturnController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: SaleMasterReturn
        public ActionResult Index()
        {
            var SaleMasterReturnlist = _context.tbl_SaleMasterReturn.ToList();
            return View(SaleMasterReturnlist);

        }
        public ActionResult New(SaleMasterReturn salemasterR)
        {
            var viewModel = new returnVM

            {
                SaleMasterR = salemasterR,
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetailRlist = _context.tbl_SaleDetailReturn.ToList(),
                Customerlist = _context.tbl_Customer.ToList(),


            };
                
            return View(viewModel);
        }
        public ActionResult Save(returnVM returnvm, decimal[] id, string[] Name, decimal[] Qty, decimal[] Price, decimal[] Total)
        {
            if (returnvm.SaleMasterR.id == 0)
            {
                returnvm.SaleMasterR.id = _context.Database.SqlQuery<decimal>("SELECT ISNULL(MAX(id), 0) + 1 AS id FROM SaleMasterReturns").FirstOrDefault();

                for (int i = 0; i < Name.Count(); i++)
                {

                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetailReturns(itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + returnvm.SaleMasterR.id + ")");

                }
                _context.tbl_SaleMasterReturn.Add(returnvm.SaleMasterR);

            }
            else
            {
                var SaleMasterReturndb = _context.tbl_SaleMasterReturn.Single(c => c.id == returnvm.SaleMasterR.id);
                SaleMasterReturndb.date = returnvm.SaleMasterR.date;
                SaleMasterReturndb.customerName = returnvm.SaleMasterR.customerName;
                SaleMasterReturndb.grosstotal = returnvm.SaleMasterR.grosstotal;
                SaleMasterReturndb.discount = returnvm.SaleMasterR.discount;
                SaleMasterReturndb.receivedamount = returnvm.SaleMasterR.receivedamount;
                SaleMasterReturndb.comment = returnvm.SaleMasterR.comment;
                SaleMasterReturndb.balanceamount = returnvm.SaleMasterR.balanceamount;
                SaleMasterReturndb.grandtotal = returnvm.SaleMasterR.grandtotal;
                SaleMasterReturndb.nettotal = returnvm.SaleMasterR.nettotal;
                SaleMasterReturndb.tax = returnvm.SaleMasterR.tax;
                for (int i = 0; i < Name.Count(); i++)
                {
                    decimal mid = returnvm.SaleMasterR.id;

                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetailReturns (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + mid + ")");
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


            return RedirectToAction("Index", "SaleMasterReturn");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var SalesMasterR = _context.tbl_SaleMasterReturn.SingleOrDefault(c => c.id == Id);
            //if (SalesMaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new returnVM
            {
                Customerlist = _context.tbl_Customer.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetailRlist = _context.Database.SqlQuery<SaleDetailReturn>("SELECT * FROM SaleDetailReturns where mid = " + Id),
                SaleMasterR = SalesMasterR
            };
            return View("New", ViewModel);

            //    return View("New", SalesMaster);
        }
        public ActionResult Print(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var SalesMasterR = _context.tbl_SaleMasterReturn.SingleOrDefault(c => c.id == Id);
            //if (SalesMaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new returnVM
            {
                Customerlist = _context.tbl_Customer.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetailRlist = _context.Database.SqlQuery<SaleDetailReturn>("SELECT * FROM SaleDetailReturns where mid = " + Id),
                SaleMasterR = SalesMasterR
            };
            return View("Print", ViewModel);

            //    return View("New", SalesMaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_SaleMasterReturn.SingleOrDefault(c => c.id == Id);
            //_context.Database.SqlQuery<SaleDetail>("DELETE  FROM SaleDetails where mid = " + Id);
            _context.Database.ExecuteSqlCommand("DELETE FROM SaleDetailReturn where mid = " + dept.id);
            _context.tbl_SaleMasterReturn.Remove(dept);

            _context.SaveChanges();
            return RedirectToAction("Index", "SaleMasterReturn");
        }
    }
}