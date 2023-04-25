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
    public class PurchaseReportController : Controller
    {
        private Models.ApplicationDbContext _context;

        public PurchaseReportController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: PurchaseReport
        public ActionResult Index(PurchaseReport PurchaseReport)
        {
            string tablename = PurchaseReport.tablename;
        
            DateTime df = PurchaseReport.dateFrom;
            DateTime dt = PurchaseReport.dateTo;
            if (PurchaseReport.tablename == "Pmaster" && PurchaseReport.view == "InvoiceSumm")
            {

                var report = _context.Database.SqlQuery<Pmaster>("SELECT * FROM Pmasters where Pmasters.date BETWEEN '" + df + "' AND '" + dt + "'").ToList();
            return View(report);
            }
            else if (PurchaseReport.tablename == "PmasterR" && PurchaseReport.view == "InvoiceSumm")
            {
                return RedirectToAction("Index2", PurchaseReport);

            }
            else if (PurchaseReport.tablename == "PDetail" && PurchaseReport.view == "ItemDetail")
            {
                return RedirectToAction("Index3", PurchaseReport);

            }
            else if (PurchaseReport.tablename == "Pmaster" && PurchaseReport.view == "InvoiceDetail")
            {
                return RedirectToAction("Index4", PurchaseReport);

            }
            else if (PurchaseReport.tablename == "PmasterR" && PurchaseReport.view == "InvoiceDetail")
            {
                return RedirectToAction("Index5", PurchaseReport);

            }

            else
            {
                return View();
            }

        }
        public ActionResult Index2(PurchaseReport PurchaseReport)
        {
            string tablename = PurchaseReport.tablename;

            DateTime df = PurchaseReport.dateFrom;
            DateTime dt = PurchaseReport.dateTo;
            

                var report1 = _context.Database.SqlQuery<PmasterR>("SELECT * FROM PmasterRs where PmasterRs.date BETWEEN '" + df + "' AND '" + dt + "'").ToList();
                return View(report1);
            
        }
        public ActionResult Index3(PurchaseReport PurchaseReport)
        {
            var report2 = _context.Database.SqlQuery<PDetail>("SELECT * FROM PDetails ").ToList();
            return View(report2);

        }
        public ActionResult Index4(PurchaseReport PurchaseReport)
        {
            string tablename = PurchaseReport.tablename;

            DateTime df = PurchaseReport.dateFrom;
            DateTime dt = PurchaseReport.dateTo;
            var viewModel = new ReportVM

            {
                Pmasterlist = _context.Database.SqlQuery<Pmaster>("SELECT * FROM Pmasters where Pmasters.date BETWEEN '" + df + "' AND '" + dt + "'").ToList(),
                PDetaillist = _context.Database.SqlQuery<PDetail>("SELECT pd.mid, pd.itemname,pd.itemid, pd.saleprice, pd.total, pd.quantity, pm.id ,pm.date FROM PDetails pd, Pmasters pm where pm.date BETWEEN '" + df + "' AND '" + dt + "' AND pm.id=pd.mid ").ToList(),
                PurchaseReport = PurchaseReport,
                final = _context.Database.SqlQuery<decimal>("Select isnull(sum(nettotal), 0) from Pmasters where Pmasters.date BETWEEN '" + df + "' AND '" + dt + "' ").FirstOrDefault()

        };
            return View(viewModel);

        }
        public ActionResult Index5(PurchaseReport PurchaseReport)
        {
            string tablename = PurchaseReport.tablename;

            DateTime df = PurchaseReport.dateFrom;
            DateTime dt = PurchaseReport.dateTo;
            var viewModel = new ReportVM

            {
                PmasterRlist = _context.Database.SqlQuery<PmasterR>("SELECT * FROM PmasterRs where PmasterRs.date BETWEEN '" + df + "' AND '" + dt + "'").ToList(),
                PdetailRlist = _context.Database.SqlQuery<PdetailR>("SELECT pd.mid, pd.itemname,pd.itemid, pd.saleprice, pd.total, pd.quantity, pm.id ,pm.date FROM PdetailRs pd, PmasterRs pm where pm.date BETWEEN '" + df + "' AND '" + dt + "' AND pm.id=pd.mid ").ToList(),
                PurchaseReport = PurchaseReport,
                final1 = _context.Database.SqlQuery<decimal>("Select isnull(sum(nettotal), 0) from PmasterRs where PmasterRs.date BETWEEN '" + df + "' AND '" + dt + "' ").FirstOrDefault()

            };
            return View(viewModel);

        }



        public ActionResult New(PurchaseReport PurchaseReport)
        {
            var viewModel = new ReportVM

            {
                Pmasterlist = _context.tbl_Pmaster.ToList(),
                PurchaseReport = PurchaseReport,
                Invoicelist = _context.tbl_Invoice.ToList(),
                Tablelist = _context.tbl_Table.ToList(),
            };
            return View(viewModel);
        }
    }
}