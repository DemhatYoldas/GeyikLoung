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

        public ActionResult Index(int page = 1)
        {
            var categories = _context.Categories.ToList(); // Kategorileri veritabanından alıyoruz
            int pageSize = 5; // Her sayfada gösterilecek kategori sayısı

            var categoriesPaged = categories.ToPagedList(page, pageSize); // Sayfalama işlemi
            return View(categoriesPaged);
        }

        public ActionResult Create()
        {
            // Kategori listesi
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name");

            // Alt kategori modelini view'a göndermeye gerek yok, çünkü kategori için alt kategori girilecek.
            var categoryModel = new Category();
            return View(categoryModel); // Category modelini gönderiyoruz
        }

        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase imageFile, string altKategoriName)
        {
            if (imageFile != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                imageFile.SaveAs(imagePath);
                category.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
            }

            if (ModelState.IsValid)
            {
                // Kategoriyi kaydediyoruz
                _context.Categories.Add(category);
                _context.SaveChanges();

                // Alt kategori eklemek
                if (!string.IsNullOrEmpty(altKategoriName))
                {
                    var altKategori = new AltKategori
                    {
                        Name = altKategoriName,
                        CategoryId = category.Id
                    };

                    // Alt kategoriyi kaydediyoruz
                    _context.AltKategoris.Add(altKategori);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            // Eğer model geçerli değilse, alt kategori seçimi için tekrar listeyi gönder
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Alt kategorileri de view'a gönderelim
            ViewBag.AltKategoriler = new SelectList(_context.AltKategoris.Where(a => a.CategoryId == id), "Id", "Name");

            return View(category); // Category modelini gönderiyoruz
        }

        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase imageFile, string altKategoriName)
        {
            var existingCategory = _context.Categories.Find(category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.ImagePath = category.ImagePath;

                if (imageFile != null)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                    imageFile.SaveAs(imagePath);
                    existingCategory.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
                }

                _context.SaveChanges();

                // Alt kategori eklemek
                if (!string.IsNullOrEmpty(altKategoriName))
                {
                    var altKategori = new AltKategori
                    {
                        Name = altKategoriName,
                        CategoryId = category.Id
                    };
                    _context.AltKategoris.Add(altKategori);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AltKategoriler = new SelectList(_context.AltKategoris.Where(a => a.CategoryId == category.Id), "Id", "Name");
            return View(category); // Category modelini tekrar gönderiyoruz
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