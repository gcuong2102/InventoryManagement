using InventoryManagerment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class ListReceiptImportController : BaseController
    {
        // GET: ListReceiptImport
        public ActionResult Index(string searchString, string nameProduct, string staffName, string note, DateTime? dateImport, int status = 2, int page = 1, int pageSize = 10)
        {
            ViewBag.Title = "Tuấn Hoan - Danh Sách Phiếu nhập";
            TempData[Common.CommonConstants.PAGE_NAME] = "Danh sách phiếu nhập";
            ViewBag.searchString = searchString;
            ViewBag.nameProduct = nameProduct;
            ViewBag.note = note;
            ViewBag.status = status;
            ViewBag.pageSize = pageSize;
            bool? stt = null;
            if (status == 1)
            {
                stt = true;
            }
            else if (status == 0)
            {
                stt = false;
            }
            else
            {
                stt = null;
            }
            if (dateImport.HasValue)
            {
                ViewBag.dateImport = dateImport.Value.ToString("yyyy-MM-dd");
            }
            var model = new DataAccess().ListAllReceiptImportOnPagedlist(searchString, note, nameProduct,GetUserName(), GetUserName(), dateImport, stt, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Nhập";
            var model = new DataAccess().GetImportForm(id);
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(List<ListImport> model)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Nhập";
            int result = new DataAccess().UpdateImportForStaff(model, GetUserName());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}