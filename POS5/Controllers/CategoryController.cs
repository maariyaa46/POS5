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
    public class CategoryController : Controller
    {
        private Models.ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Category
        public ActionResult Index()
        {
            var Categorylist = _context.tbl_Category.ToList();
          
            return View(Categorylist);

        }
        [HttpGet]
        public ActionResult New()
        {
            ViewBag.BrandList = new SelectList(_context.tbl_Brand, "id", "name");
            return View();

    }
    [HttpPost]
        public ActionResult New(Category Category)
        {
            string filename = Path.GetFileNameWithoutExtension(Category.imgFile.FileName);
            string extenstion = Path.GetExtension(Category.imgFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extenstion;
            Category.path = "~/Image/" + filename;

            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            Category.imgFile.SaveAs(filename);


            if (Category.id == 0)
            {

                _context.tbl_Category.Add(Category);
            }
            else
            {
                var Categorydb = _context.tbl_Category.Single(c => c.id == Category.id);
                Categorydb.name = Category.name;
   
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


            return RedirectToAction("New", "Category");
        }
        //public ActionResult Edit(int Id)
        //{
        //    var category = _context.tbl_Category.SingleOrDefault(c => c.id == Id);
        //    if (category == null)
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

        //    var viewModels = new ItemVM
        //    {
               
        //        Brandlist = _context.tbl_Brand.ToList(),
        //        Category = category
        //    };
        //    return View("New", viewModels);
        //}
        //public ActionResult Delete(int Id)
        //{
        //    var dept = _context.tbl_Category.SingleOrDefault(c => c.id == Id);
        //    _context.tbl_Category.Remove(dept);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Category");
        //}
    }
}