using InventoryManagerment.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index(string searchString,long quantity = 0,long typeProduct=0,int page=1,int pageSize=10)
        {
            ViewBag.Title = "Tuấn Hoan - Dash Board";
            TempData[Common.CommonConstants.PAGE_NAME] = "Trang chủ";
            ViewBag.User = new DataAccess().GetUser(GetUserName());
            ViewBag.searchString = searchString;
            ViewBag.typeProduct = typeProduct;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var model = new DataAccess().ListAllProductOnPagedlist(searchString,quantity, typeProduct, page, pageSize);
            return View(model);
        }
        public ActionResult Test()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult topNav()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult leftMenu()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult leftMenuStaff()
        {
            return PartialView();
        }
        [HttpPost]
        public void ExportDataProduct(string searchString, long quantity = 0, long typeProduct = 0)
        {
            var data = new DataAccess().GetDataProductExcel(searchString, quantity, typeProduct);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ProductData");
                int rowIndex = 1;
                int columnIndex = 1;
                int dataRange = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    if (i % 50 == 0 && i % 100 != 0)
                    {
                        columnIndex = 5;
                        int temp = dataRange;
                        dataRange += 50;
                        rowIndex = temp + 1;
                    }
                    if (i % 100 == 0)
                    {
                        columnIndex = 1;
                        rowIndex = dataRange + 1;
                    }

                    var font = worksheet.Cells[rowIndex, columnIndex, rowIndex, columnIndex + 2].Style.Font;
                    font.Size = 16; // Kích thước font
                    font.Name = "Times New Roman"; // Tên font chữ

                    worksheet.Cells[rowIndex, columnIndex].Value = data[i].FullName;
                    worksheet.Cells[rowIndex, columnIndex + 1].Value = data[i].Quantity;
                    worksheet.Cells[rowIndex, columnIndex + 2].Value = data[i].UnitName;
                    rowIndex++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Product.xlsx");

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}