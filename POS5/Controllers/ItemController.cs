using POS5.Models;
using POS5.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS5.Controllers
{
    public class ItemController : Controller
    {
        private Models.ApplicationDbContext _context;

        public ItemController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Item
        public ActionResult Index()
        {
            var Itemlist = _context.tbl_Item.ToList();
            return View(Itemlist);

        }
        public ActionResult New(Item item)
        {
            var viewModel = new ItemVM

            {
                Categorylist = _context.tbl_Category.ToList(),
                Brandlist = _context.tbl_Brand.ToList(),
                Item = item
            };
            return View(viewModel);
        }

        public ActionResult Save(ItemVM ItemVM)
        {

            if (ItemVM.Item.id == 0)
            {

                _context.tbl_Item.Add(ItemVM.Item);
            }
            else
            {
                var Itemdb = _context.tbl_Item.Single(c => c.id == ItemVM.Item.id);
                Itemdb.name = ItemVM.Item.name;
                Itemdb.description = ItemVM.Item.description;
                Itemdb.price = ItemVM.Item.price;
                Itemdb.catagoryname = ItemVM.Item.catagoryname;

                Itemdb.brandname = ItemVM.Item.brandname;
                Itemdb.shelfno = ItemVM.Item.shelfno;
                Itemdb.stocklevel = ItemVM.Item.stocklevel;
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


            return RedirectToAction("New", "Item");
        }
        public ActionResult Edit(int Id)
        {
            var item = _context.tbl_Item.SingleOrDefault(c => c.id == Id);
            if (item == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var viewModels = new ItemVM
            {
                Categorylist = _context.tbl_Category.ToList(),
                Brandlist = _context.tbl_Brand.ToList(),
                Item = item
            };
            return View("New",viewModels);
        }
        public ActionResult Delete(int Id)
        {
            var dept = _context.tbl_Item.SingleOrDefault(c => c.id == Id);
            _context.tbl_Item.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("Index", "Item");
        }
    }
}
