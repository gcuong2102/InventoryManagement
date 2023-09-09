using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryManagerment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GuiPhieuGiaoHang",
                url: "guiphieugiaohang",
                defaults: new { controller = "ReceiveBill", action = "Post" }
            );
            routes.MapRoute(
                name: "XemPhieuGiaoHang",
                url: "xemphieugiaohang",
                defaults: new { controller = "ReceiveBill", action = "Index" }
            );
            routes.MapRoute(
                name: "QuanLyKho",
                url: "quanlykho",
                defaults: new { controller = "Stock", action = "index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
