using GeyikLoung.Context;
using GeyikLoung.Entities;
using PagedList;
using PagedList.Mvc;
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

        // Alt kategorileri kategoriye göre getiren aksiyon
        public ActionResult GetAltKategorilerByCategory(int categoryId)
        {
            var altKategoriler = _context.AltKategoris
                                          .Where(a => a.CategoryId == categoryId)
                                          .Select(a => new { a.Id, a.Name })
                                          .ToList();

            return Json(altKategoriler, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index(int? page)
        {
            // Veritabanındaki tüm ürünleri alıyoruz
            var products = _context.Products.Include("Category").ToList();

            // Sayfalama için varsayılan değer olarak 1. sayfa ve sayfa başına 5 ürün gösteriyoruz
            int pageSize = 5;
            int pageNumber = (page ?? 1); // Sayfa numarasını URL'den alıyoruz

            // PagedList kullanarak ürünleri sayfalıyoruz
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");

            // Başlangıçta alt kategori listesi boş
            ViewBag.AltKategoriId = new SelectList(Enumerable.Empty<SelectListItem>());

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            try
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
                ViewBag.AltKategoriId = new SelectList(_context.AltKategoris, "Id", "Name", product.AltKategoriId);

                return View(product);
            }
            catch (Exception ex)
            {
                // Hata mesajını logla veya kullanıcıya göster
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                return View(product);
            }
        }


        public ActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            // Seçilen kategoriye göre alt kategoriler
            var altKategoriler = _context.AltKategoris.Where(a => a.CategoryId == product.CategoryId).ToList();
            ViewBag.AltKategoriId = new SelectList(altKategoriler, "Id", "Name", product.AltKategoriId);

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
                existingProduct.AltKategoriId = product.AltKategoriId;

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
            var altKategoriler = _context.AltKategoris.Where(a => a.CategoryId == product.CategoryId).ToList();
            ViewBag.AltKategoriId = new SelectList(altKategoriler, "Id", "Name", product.AltKategoriId);

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