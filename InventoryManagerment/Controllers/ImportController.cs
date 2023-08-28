using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagerment.Common;
using InventoryManagerment.Models;
using InventoryManagerment.Models.EF;
using InventoryManagerment.Models.List;

namespace InventoryManagerment.Controllers
{
    public class ImportController : BaseController
    {
        // GET: Import
        public ActionResult Index(string searchString,string productName,string note,DateTime? dateImport, int page = 1,int pageSize=10)
        {
            if(GetUser().RoleID != 1)
            {
                return RedirectToAction("Index", "Error");
            }
            TempData[Common.CommonConstants.PAGE_NAME] = "Phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Phiếu Nhập";
            ViewBag.searchString = searchString;
            ViewBag.note = note;
            ViewBag.productName = productName;
            if (dateImport.HasValue)
            {
                ViewBag.dateImport = dateImport.Value.ToString("yyyy-MM-dd");
            }
            if (pageSize > 10)
            {
                ViewBag.pageSize = pageSize;
            }
            var model = new DataAccess().ListAllImportOnPagedList(searchString,productName,note, dateImport,false ,page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if(GetUser().RoleID == 3)
            {
                return RedirectToAction("Index", "Error");
            }
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Thêm Phiếu Nhập";
            var dao = new DataAccess();
            ViewBag.UnitID = new SelectList(dao.ListAllUnitToViewBag(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(dao.ListAllCategoryToViewBag(), "ID", "Name");
            ViewBag.PackageID = new SelectList(dao.ListtAllPackageToViewBag(), "ID", "Name");
            SetViewBag();
            return View();
        }
        [HttpPost]
        public JsonResult Create(List<ProductModel> listProduct)
        {
            ViewBag.Title = "Tuấn Hoan - Thêm Phiếu Nhập";
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm phiếu nhập";
            SetViewBag();
            bool result = new DataAccess().InsertImport(listProduct,GetUserName());
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateProduct(Product product)
        {
            var rs = new DataAccess().InsertProduct(product, GetUserName());
            return Json(rs,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void CreateSupplier (string name,string code)
        {
            var model = new Supplier();
            model.Name = name;
            model.Code = code;
            new DataAccess().InsertSupplier(model,GetUserName());
        }
        [HttpGet]
        public ActionResult Edit(long id) 
        {
            if (GetUser().RoleID != 1)
            {
                return RedirectToAction("Index", "Error");
            }
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Nhập";
            SetViewBag();
            var model = new DataAccess().GetAllProduct(id);
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(List<ProductModel> model)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa phiếu nhập";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Phiếu Nhập";
            SetViewBag();
            bool result = new DataAccess().UpdateImport(model,GetUserName());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            new DataAccess().DeleteImport(id,GetUserName());
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult GetDataImport(string code)
        {
            var listImport = new DataAccess().GetDataImport(code);
            return Json(listImport, JsonRequestBehavior.AllowGet);
        }
        public void SetViewBag(long? id = null)
        {
            ViewBag.SupplierID = new SelectList(new DataAccess().ListAllSupplierToViewBag(), "ID", "Name", id);
            ViewBag.Supplier = new DataAccess().ListAllSupplierToViewBag();
            ViewBag.Unit = new DataAccess().ListAllUnitToViewBag();
            ViewBag.Package = new DataAccess().ListallPackageToViewBag();
            ViewBag.Product = new DataAccess().ListAllProductToViewBag();
        }
        [HttpGet]
        public JsonResult SetAutoComplete()
        {
            var rs = new DataAccess().GetListDataImport();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void ChangeStatus(string code)
        {
            code = code.Substring(1, code.Length - 1);
            new DataAccess().ChangeStatus(code, "import", GetUserName());
        }
        [HttpGet]
        public JsonResult SetAutoCompleteForSearch()
        {
            var result = new DataAccess().GetListAutoCompleteForImportIndex();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}