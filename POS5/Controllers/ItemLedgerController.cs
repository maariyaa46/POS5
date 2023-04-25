using POS5.queryModel;
using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace POS5.Controllers
{
    public class ItemLedgerController : Controller
    {
        private Models.ApplicationDbContext _context;

        public ItemLedgerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: ItemLedger
        public ActionResult Index(parameter parameter)
        {
            var viewModel = new LedgerVM
            {
                ItemLedgerList = _context.Database.SqlQuery<ItemLedger>("Select * from ItemLedgers where (SaleReturnOUT <>0 AND SaleIn <>0 AND PurchaseIn<>0 AND PurchaseReturnOut<>0 AND Balance<>0)").ToList(),


                parameter = parameter
            };
            return View(viewModel);

        }

        public ActionResult View(parameter parameter)
        {
            _context.Database.ExecuteSqlCommand("Delete from ItemLedgers");
            string name = parameter.name;

            DateTime dt = parameter.datefrom;
        
            int diff = (parameter.datet - parameter.datefrom).Days + 1;
            int i;
            for (i = 0; i < diff; i++)
            {

                var purchaseid = _context.Database.SqlQuery<decimal>("select m.id from Pmasters m, PDetails d where m.date = '" + dt + "'   AND d.itemname= '" + name + "' AND m.id=d.mid").FirstOrDefault();

                var saleid = _context.Database.SqlQuery<decimal>("select m.id from SalesMasters m where m.date = '" + dt + "' ").FirstOrDefault();
                
                var preturnid = _context.Database.SqlQuery<decimal>("select m.id from PmasterRs m, PdetailRs d where m.date = '" + dt + "'   AND d.itemname= '" + name + "' AND m.id=d.mid").FirstOrDefault();

                var sreturnid = _context.Database.SqlQuery<decimal>("select m.id from SaleMasterReturns m where m.date = '" + dt + "' ").FirstOrDefault();

                var PurchaseDetailIN = _context.Database.SqlQuery<decimal>("select isnull(sum(d.quantity), 0) from PDetails d,Pmasters m where m.date = '" + dt + "' AND d.itemname='" + name + "' AND m.id=d.mid  ").FirstOrDefault();

                var SaleDetailIN = _context.Database.SqlQuery<decimal>("select isnull(sum(r.quantity), 0) from SaleDetailReturns r,SaleMasterReturns m where m.date = '" + dt + "' AND r.itemname='" + name + "' AND m.id=r.mid").FirstOrDefault();

                var PurchaseReturnOUT = _context.Database.SqlQuery<decimal>("select isnull(sum(r.quantity), 0) from PdetailRs r,PmasterRs m where ( m.date = '" + dt + "' AND r.itemname='" + name + "' AND m.id=r.mid) ").FirstOrDefault();

                var SaleReturnOUT = _context.Database.SqlQuery<decimal>("select isnull(sum(d.quantity), 0) from SaleDetails d,SalesMasters m where (m.date = '" + dt + "' AND d.itemname='" + name + "' AND m.id=d.mid) ").FirstOrDefault();

                var purchaseRemain = PurchaseDetailIN - PurchaseReturnOUT;
                var stockRemain = purchaseRemain - SaleDetailIN + SaleReturnOUT;
                var balance = purchaseRemain - stockRemain;

                _context.Database.ExecuteSqlCommand("INSERT INTO ItemLedgers(id,pur, preturnid, sreturnid, itemname,dateF,SaleIN,PurchaseIN,SaleReturnOUT,PurchaseReturnOUT,Balance) VALUES('" + saleid + "', '" + purchaseid + "' , '" + preturnid + "','" + sreturnid + "','" + name + "','" + dt + "', '" + SaleDetailIN + "','" + PurchaseDetailIN + "','" + SaleReturnOUT + "','" + PurchaseReturnOUT + "','" + balance + "') ");
                dt = dt.AddDays(1);
                _context.SaveChanges();
            }


            return RedirectToAction("Index", parameter);
        }


    }
}