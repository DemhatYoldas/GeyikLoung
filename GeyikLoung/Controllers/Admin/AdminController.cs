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
            return View();
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
