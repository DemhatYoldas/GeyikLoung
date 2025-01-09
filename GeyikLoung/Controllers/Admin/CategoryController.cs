using GeyikLoung.Context;
using GeyikLoung.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GeyikLoung.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private readonly GeyikLoungContext _context;

        public CategoryController()
        {
            _context = new GeyikLoungContext();
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name","ImagePath");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                imageFile.SaveAs(imagePath);
                category.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name", "ImagePath");
            return View(category);

        }

        public ActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name", "Imagepath");
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase imageFile)
        {
            var existingProduct = _context.Categories.Find(category.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = category.Name;
                existingProduct.ImagePath = category.ImagePath;
                if (imageFile != null)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                    imageFile.SaveAs(imagePath);
                    existingProduct.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category = new SelectList(_context.Categories, "Id", "Name", "ImagePath");
            return View(category);

        }

        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}