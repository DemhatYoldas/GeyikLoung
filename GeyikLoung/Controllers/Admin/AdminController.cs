using GeyikLoung.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace GeyikLoung.Controllers.Admin
{
    public class AdminController : Controller
    {
        private GeyikLoungContext db = new GeyikLoungContext(); // Veritabanı bağlamı


        public ActionResult Index()
        {
            if (Session["AdminId"] == null || Session["AdminRole"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            base.OnActionExecuting(filterContext);
        }

        // Partial Views
        public PartialViewResult partialHead()
        {
            return PartialView();
        }

        public PartialViewResult partialSidebar()
        {
            return PartialView();
        }

        public PartialViewResult partialFooter()
        {
            return PartialView();
        }

        public PartialViewResult partialProfile()
        {
            return PartialView();
        }

        public PartialViewResult partialScript()
        {
            return PartialView();
        }

    }
}
