using GeyikLoung.Context;
using GeyikLoung.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GeyikLoung.Controllers.Default
{
    public class DefaultController : Controller
    {
        private readonly GeyikLoungContext _context;

        public DefaultController()
        {
            _context = new GeyikLoungContext(); // DB bağlantısını manuel kurduk
        }

        [HttpGet]
        public JsonResult GetByCategory(int categoryId)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImagePath
                })
                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public ActionResult CategoryDetails(int categoryId)
        {

            var category = _context.Categories
                            .Include(c => c.Products)
                            .Include(c => c.AltKategoriler)
                            .FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                return HttpNotFound();
            }

            if (category.AltKategoriler == null || !category.AltKategoriler.Any())
            {
                // Eğer alt kategori yoksa, ürünleri listeleyen bir sayfaya yönlendir.
                return View("ProductList", category.Products);
            }

            // Eğer alt kategoriler varsa, onları listeleyen sayfaya yönlendir.
            return View("SubCategoryList", category.AltKategoriler);


        }


        public ActionResult AltKategoriDetails(int altKategoriId)
        {

            // Burada altKategoriId parametresini kontrol edebilirsiniz
            System.Diagnostics.Debug.WriteLine("Alt Kategori ID: " + altKategoriId);

            // Alt kategoriyi ve içindeki ürünleri getiriyoruz
            var altKategori = _context.AltKategoris
                                      .Include(a => a.Products) // Alt kategoriye bağlı ürünleri getirin
                                      .FirstOrDefault(a => a.Id == altKategoriId);

            if (altKategori == null)
            {
                return HttpNotFound();
            }

            return View(altKategori); // Alt kategori detay sayfasına yönlendiriyoruz
        }



        public PartialViewResult SubCategoryList()
        {
            return PartialView();
        }


        public PartialViewResult ProductList()
        {
            return PartialView();
        }



        public PartialViewResult PartialHead()
        {
            return PartialView();
        }

        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
    }
}