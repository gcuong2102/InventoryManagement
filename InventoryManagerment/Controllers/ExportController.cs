using InventoryManagerment.Models;
using InventoryManagerment.Models.EF;
using InventoryManagerment.Models.List;
using InventoryManagerment.Models.ModelProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class ExportController : BaseController
    {
        // GET: Export
        public async Task<ActionResult> Index(string searchString,string nameProduct, string userName, string staffName,string note, DateTime? dateExport, int status = 2, int page = 1,int pageSize =10)
        {
            if (GetUser().RoleID != 1)
            {
                return RedirectToAction("Index", "Error");
            }
            User user = GetUser();
            TempData[Common.CommonConstants.PAGE_NAME] = "Phiếu xuất";
            ViewBag.Title = "Tuấn Hoan - Phiếu Xuất";
            ViewBag.searchString = searchString;
            ViewBag.nameProduct = nameProduct;
            ViewBag.userName = userName;
            ViewBag.staffName = staffName;
            ViewBag.note = note;
            ViewBag.status = status;
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
            if (dateExport.HasValue)
            {
                ViewBag.dateExport = dateExport.Value.ToString("yyyy-MM-dd");
            }
            ViewBag.pageSize = pageSize;
            var model = await new DataAccess().ListAllExportOnPagedListAsync(searchString, note, nameProduct, staffName, userName, dateExport, stt, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm phiếu xuất";
            ViewBag.Title = "Tuấn Hoan - Thêm Phiếu Xuất";
            SetViewBag();
            return View();

        }
        [HttpPost]
        public JsonResult Create(List<ListExport> model)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm phiếu xuất";
            ViewBag.Title = "Tuấn Hoan - Thêm Phiếu Xuất";
            SetViewBag();
            bool result = new DataAccess().InsertExport(model, GetUserName());
            if (result)
            {
                SetAlert("Thêm phiếu xuất thành công", "success");
            }
            else
            {
                SetAlert("Thêm phiếu xuất thất bại", "danger");
            }
            return Json("SUCCESS", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (GetUser().RoleID != 1)
            {
                return RedirectToAction("Index", "Error");
            }
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu xuất";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Xuất";
            var model = new DataAccess().GetExportForm(id);
            SetViewBag();
            User user = GetUser();
            return View(model);
        }
        [HttpPost]
        public void Edit(List<ListExport> model)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu xuất";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Xuất";
            new DataAccess().UpdateExport(model,GetUserName());
        }
        [HttpPost]
        public void Delete(long id)
        {
            User user = GetUser(); 
            if (user.RoleID == 1)
            {
                new DataAccess().DeleteExport(id,GetUserName());
            }
        }
        [HttpPost]
        public void ChangeStatus(string code)
        {
            code = code.Substring(1, code.Length-1);
            new DataAccess().ChangeStatus(code,"export",GetUserName());
        }
        public void SetViewBag()
        {
            ViewBag.Unit = new DataAccess().ListAllUnitToViewBag();
            ViewBag.Product = new DataAccess().ListAllProductToViewBag();
            ViewBag.Customer = new DataAccess().ListtAllCustomerToViewBag();
        }
        [HttpGet]
        public JsonResult GetDataExport(string code)
        {
            var listData = new DataAccess().GetDataExport(code);
            return Json(listData,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SetAutoComplete()
        {
            var result = new DataAccess().GetListDataExportAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetNameCustomer()
        {
            var list = new DataAccess2().GetNameCustomer();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SetAutoCompleteForSearch()
        {
            var result = new DataAccess().GetListAutoCompleteForExportIndex();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}