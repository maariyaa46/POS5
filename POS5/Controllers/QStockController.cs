using POS5.queryModel;
using POS5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class QStockController : Controller
    {
        private Models.ApplicationDbContext _context;

        public QStockController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: QStock
        public ActionResult Index()
        {
            var stock = _context.Database.SqlQuery<QStock>("SELECT id,name,stocklevel,PurchasedetailIN,SalesReturnIN, SalesOUT, PurchaseDetailOUT,stocklevel+PurchasedetailIN+SalesReturnIN-SalesOUT-PurchaseDetailOUT AS Total FROM (SELECT    id, name, stocklevel, (SELECT  ISNULL(SUM(quantity), 0) from PDetails where itemid = Items.id) AS PurchasedetailIN," 
                +"(SELECT ISNULL(SUM(quantity), 0) from SaleDetails WHERE itemid = Items.id) AS SalesOUT,"
                +"(SELECT ISNULL(SUM(quantity), 0)from PdetailRs WHERE itemid = Items.id) AS PurchaseDetailOUT,(SELECT ISNULL(SUM(quantity), 0)from SaleDetailReturns WHERE itemid = Items.id) AS SalesReturnIN FROM Items ) AS Random").ToList();

            return View(stock);
        }
    }
}