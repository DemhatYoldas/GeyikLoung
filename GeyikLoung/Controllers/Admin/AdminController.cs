using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeyikLoung.Controllers.Admin
{
    public class AdminController : Controller
    {
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (Session["AdminId"] == null)
        //    {
        //        filterContext.Result = RedirectToAction("Index", "AdminLogin");
        //    }

        //    base.OnActionExecuting(filterContext);
        //}


        //[Authorize]// Yalnızca giriş yapmış kullanıcıların erişebileceği sayfa
        public ActionResult Index()
        {
            return View();
        }

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