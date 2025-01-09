using GeyikLoung.Context;
using GeyikLoung.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeyikLoung.Controllers.Admin
{
    public class ProductController : Controller
    {
        private readonly GeyikLoungContext _context;

        public ProductController()
        {
            _context = new GeyikLoungContext();
        }

        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                imageFile.SaveAs(imagePath);
                product.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase imageFile)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                if (imageFile != null)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                    imageFile.SaveAs(imagePath);
                    existingProduct.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}