using GeyikLoung.Context;
using GeyikLoung.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GeyikLoung.Controllers
{
    public class AccountController : Controller
    {
        private GeyikLoungContext db = new GeyikLoungContext();

        // Kayıt Ol Sayfası
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Kayıt Ol İşlemi
        [HttpPost]
        public ActionResult Register(AdminUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kullanıcı adı kontrolü
                    if (db.adminUsers.Any(u => u.Username == user.Username))
                    {
                        ModelState.AddModelError("Username", "Bu kullanıcı adı zaten alınmış.");
                        return View(user);
                    }

                    // Varsayılan rolü "Admin" olarak ayarla
                    user.Role = "Admin";

                    db.adminUsers.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kayıt işlemi sırasında bir hata oluştu: " + ex.Message);
                }
            }

            return View(user);
        }

        // Giriş Yap Sayfası
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Giriş Yap İşlemi
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var admin = db.adminUsers.FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                // Oturum açma işlemi (Session kullanarak)
                Session["AdminId"] = admin.Id;
                Session["AdminRole"] = admin.Role;

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        // Çıkış Yap İşlemi
        public ActionResult Logout()
        {
            Session.Clear(); // Oturumu temizle
            Session.Abandon(); // Oturumu tamamen sonlandır

            // Tarayıcı önbelleğini temizle
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Login", "Account");
        }



    }
}