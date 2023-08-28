using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Lỗi truy cập 404";
            ViewBag.Title = "Tuấn Hoan - Không có quyền truy cập";
            return View();
        }
    }
}