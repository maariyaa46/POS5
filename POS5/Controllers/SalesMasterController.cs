using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class SalesMasterController : Controller
    {
        private Models.ApplicationDbContext _context;

        public SalesMasterController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: SalesMaster
        public ActionResult Index()
        {
            var SalesMasterlist = _context.tbl_SalesMaster.ToList();
            return View(SalesMasterlist);

        }
        public ActionResult New(SalesMaster saleMaster)
        {
            var viewModel = new newSaleVM

            {
                SaleMaster = saleMaster,
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetaillist = _context.tbl_SaleDetail.ToList(),
                Customerlist= _context.tbl_Customer.ToList(),


            };
            return View(viewModel);
        }

        public ActionResult Save(newSaleVM SaleVM, decimal[] id, string[] Name, decimal[] Qty, decimal[] Price, decimal[] Total)
        {
            if (SaleVM.SaleMaster.id == 0)
            {
                SaleVM.SaleMaster.id = _context.Database.SqlQuery<decimal>("SELECT ISNULL(MAX(id), 0) + 1 AS id FROM SalesMasters").FirstOrDefault();

                for (int i = 0; i < Name.Count(); i++)
                {

                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetails (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + SaleVM.SaleMaster.id + ")");

                }
                _context.tbl_SalesMaster.Add(SaleVM.SaleMaster);

            }
            else
            {
                var SalesMasterdb = _context.tbl_SalesMaster.Single(c => c.id == SaleVM.SaleMaster.id);
                SalesMasterdb.date = SaleVM.SaleMaster.date;
                SalesMasterdb.customerName = SaleVM.SaleMaster.customerName;
                
                SalesMasterdb.grosstotal = SaleVM.SaleMaster.grosstotal;
                SalesMasterdb.discount = SaleVM.SaleMaster.discount;
                SalesMasterdb.receivedamount = SaleVM.SaleMaster.receivedamount;
                
                SalesMasterdb.balanceamount = SaleVM.SaleMaster.balanceamount;
                SalesMasterdb.nettotal = SaleVM.SaleMaster.nettotal;
                SalesMasterdb.grandtotal = SaleVM.SaleMaster.grandtotal;
                SalesMasterdb.tax = SaleVM.SaleMaster.tax;

                for (int i = 0; i< Name.Count(); i++)
                {
                    decimal mid = SaleVM.SaleMaster.id;

                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetails (itemid, itemname, quantity, saleprice, total, mid)  VALUES ('" + id[i] + "','" + Name[i] + "','" + Qty[i] + "','" + Price[i] + "','" + Total[i] + "'," + mid + ")");
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


            return RedirectToAction("Index", "SalesMaster");
        }
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var SalesMaster = _context.tbl_SalesMaster.SingleOrDefault(c => c.id == Id);
           
            var ViewModel = new newSaleVM
            {
                Customerlist = _context.tbl_Customer.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetaillist = _context.Database.SqlQuery<SaleDetail>("SELECT * FROM SaleDetails where mid = " + Id),
                SaleMaster = SalesMaster
            };
            return View("New" , ViewModel);
        }
        public ActionResult Print(int Id)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var SalesMaster = _context.tbl_SalesMaster.SingleOrDefault(c => c.id == Id);
            //if (SalesMaster == null)
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var ViewModel = new newSaleVM
            {
                Customerlist = _context.tbl_Customer.ToList(),
                Itemlist = _context.tbl_Item.ToList(),
                SaleDetaillist = _context.Database.SqlQuery<SaleDetail>("SELECT * FROM SaleDetails where mid = " + Id),
                SaleMaster = SalesMaster

            };
            return View("Print", ViewModel);

            //    return View("Print", SalesMaster);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_SalesMaster.SingleOrDefault(c => c.id == Id);
            //_context.Database.SqlQuery<SaleDetail>("DELETE  FROM SaleDetails where mid = " + Id);
            _context.Database.ExecuteSqlCommand("DELETE FROM SaleDetails where mid = " + dept.id);
            _context.tbl_SalesMaster.Remove(dept);

            _context.SaveChanges();
            return RedirectToAction("Index", "SalesMaster");
        }
    }
}