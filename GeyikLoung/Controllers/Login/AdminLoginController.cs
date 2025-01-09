using System.Linq;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using GeyikLoung.Context;
using System;

namespace GeyikLoung.Controllers.Login
{
    public class AdminLoginController : Controller
    {
        private GeyikLoungContext db = new GeyikLoungContext();

       
        public ActionResult Index()
        {
            return View();
        }

        //// POST: AdminLogin
        //[HttpPost]
        //public ActionResult Index(string userName, string password)
        //{
        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //    {
        //        ViewBag.Message = "Kullanıcı adı ve şifre gereklidir.";
        //        return View();
        //    }

        //    var admin = db.Admins.FirstOrDefault(a => a.UserName == userName);

        //    if (admin == null)
        //    {
        //        ViewBag.Message = "Kullanıcı adı bulunamadı.";
        //        return View();
        //    }

        //    if (!VerifyPassword(password, admin.Password))
        //    {
        //        ViewBag.Message = "Şifre hatalı.";
        //        return View();
        //    }

        //    // Giriş başarılı, admin paneline yönlendir
        //    Session["AdminId"] = admin.AdminId;
        //    return RedirectToAction("Index", "Admin");
        //}

        //// Şifre doğrulama (Hash karşılaştırması)
        //private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var enteredPasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
        //        return enteredPasswordHash == storedPasswordHash;
        //    }
        //}

        //public ActionResult Logout()
        //{
        //    Session.Clear();
        //    return RedirectToAction("Index", "AdminLogin");
        //}

    }
}
