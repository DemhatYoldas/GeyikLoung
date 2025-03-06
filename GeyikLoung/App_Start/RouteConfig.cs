using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeyikLoung
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // CategoryDetails route'u
            routes.MapRoute(
                name: "CategoryDetails",
                url: "Category/Details/{categoryId}",
                defaults: new { controller = "Default", action = "CategoryDetails", categoryId = UrlParameter.Optional },
                constraints: new { categoryId = @"\d+" }  // categoryId sadece rakam olmalı
            );

            // AltKategoriDetails route'u
            routes.MapRoute(
                name: "AltKategoriDetails",
                url: "AltKategori/Details/{altKategoriId}",
                defaults: new { controller = "Default", action = "AltKategoriDetails", altKategoriId = UrlParameter.Optional },
                constraints: new { altKategoriId = @"\d+" }  // altKategoriId sadece rakam olmalı
            );

            // Default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );

            // GeyikLoung sayfasını varsayılan olarak ayarlama
            routes.MapRoute(
                name: "GeyikLoung",
                url: "",
                defaults: new { controller = "GeyikLoung", action = "Index" }
            );
        }
    }
}
