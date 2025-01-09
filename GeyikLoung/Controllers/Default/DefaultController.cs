using GeyikLoung.Context;
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