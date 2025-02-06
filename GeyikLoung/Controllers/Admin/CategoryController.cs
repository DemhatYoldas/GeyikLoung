using GeyikLoung.Context;
using GeyikLoung.Entities;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Create(Category category, HttpPostedFileBase imageFile, List<string> altKategoriNames)
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
                _context.SaveChanges(); // Önce kategoriyi kaydetmeliyiz ki ID oluşsun.

                // **Birden fazla alt kategori ekleme işlemi**
                if (altKategoriNames != null && altKategoriNames.Any())
                {
                    foreach (var altKategoriName in altKategoriNames)
                    {
                        if (!string.IsNullOrWhiteSpace(altKategoriName)) // Boş alt kategoriler eklenmesin
                        {
                            var altKategori = new AltKategori
                            {
                                Name = altKategoriName,
                                CategoryId = category.Id
                            };
                            _context.AltKategoris.Add(altKategori);
                        }
                    }
                    _context.SaveChanges(); // Değişiklikleri kaydet
                }

                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            //_context.Categories.Include(c => c.AltKategoriler).FirstOrDefault(c => c.Id == id)

            var category = _context.Categories.Include(a=>a.AltKategoriler).FirstOrDefault(c=>c.Id==id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }




        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase imageFile, List<int> altKategoriIds, List<string> altKategoriNames)
        {
            var existingCategory = _context.Categories.Include(c => c.AltKategoriler).FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
                return HttpNotFound();

            existingCategory.Name = category.Name;

            if (imageFile != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(imageFile.FileName));
                imageFile.SaveAs(imagePath);
                existingCategory.ImagePath = "/images/" + Path.GetFileName(imageFile.FileName);
            }

            if (altKategoriNames != null && altKategoriNames.Any())
            {
                for (int i = 0; i < altKategoriNames.Count; i++)
                {
                    string name = altKategoriNames[i];
                    int id = (altKategoriIds != null && i < altKategoriIds.Count) ? altKategoriIds[i] : 0;

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        if (id > 0)
                        {
                            // Mevcut alt kategoriyi güncelle
                            var existingAltKategori = existingCategory.AltKategoriler.FirstOrDefault(ak => ak.Id == id);
                            if (existingAltKategori != null)
                            {
                                existingAltKategori.Name = name;
                            }
                        }
                        else
                        {
                            // Yeni alt kategori ekle
                            existingCategory.AltKategoriler.Add(new AltKategori { Name = name });
                        }
                    }
                }
            }

            // Silinen alt kategorileri kaldır
            var altKategoriIdsSet = altKategoriIds?.ToHashSet() ?? new HashSet<int>();
            existingCategory.AltKategoriler.Where(ak => !altKategoriIdsSet.Contains(ak.Id));

            _context.SaveChanges();
            return RedirectToAction("Index");
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