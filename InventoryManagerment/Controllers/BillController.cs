﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult Index(string billCode, string searchString,string nameProduct, string totalPrice, DateTime? dateBill, int page=1,int pageSize=10)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Hóa đơn";
            ViewBag.Title = "Tuấn Hoan - Danh Sách Hóa Đơn";
            ViewBag.searchString = searchString;
            ViewBag.nameProduct = nameProduct;
            ViewBag.totalPrice = totalPrice;
            if (dateBill.HasValue)
            {
                ViewBag.dateBill = dateBill.Value.ToString("yyyy-MM-dd");
            }
            if(pageSize > 10)
            {
                ViewBag.pageSize = pageSize;
            }
            var model = new DataAccess2().ListAllHoaDonToPagedList(billCode,searchString,nameProduct,totalPrice, dateBill, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public JsonResult GetDataBill(string code)
        {
            var listBill = new DataAccess2().GetListBill(code);
            return Json(listBill, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetAutoCompleteForIndex()
        {
            var result = new DataAccess2().GetNameProduct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}