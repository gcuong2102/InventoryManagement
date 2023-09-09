using InventoryManagerment.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class ReceiveBillController : BaseController
    {
        // GET: ReceiveBill
        public ActionResult Index(DateTime? time,string customerName, string staffName, string note, string address ,int page = 1, int pageSize = 15)
        {
            var model = new DataAccess().GetDataReceiveBill(time,customerName,  staffName,  note,  address,page, pageSize);
            return View(model);
        }

        public ActionResult Post()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadImages(IEnumerable<HttpPostedFileBase> images, DateTime txtTime, string customerName, string note, double latitude, double longitude, string address)
        {
            try
            {
                // Tạo tên thư mục duy nhất dựa trên tên khách hàng và timestamp
                string uniqueFolderName = customerName.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string folderPath = Path.Combine(@"\\103.116.105.192\ReceivedBill", uniqueFolderName);

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var listUrl = new List<string>();
                foreach (var file in images)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = customerName.Trim() + Functions.GenerateUniqueCode() + Path.GetExtension(file.FileName);
                        var path = Path.Combine(folderPath, fileName); // Lưu vào thư mục duy nhất
                        file.SaveAs(path);
                        listUrl.Add(path);
                    }
                }
                var username = GetUserName();
                var result = new DataAccess().UploadImages(listUrl, txtTime, customerName, note, GetUserName(), latitude, longitude, address);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddressList(int page = 1,int pageSize = 30)
        {
            var model = new DataAccess().GetAddressList(page,pageSize);
            return View(model);
        }
        [HttpGet]
        public JsonResult GetNameCustomer()
        {
            var result = new DataAccess2().GetNameCustomer();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetImagesByDirectory(string code)
        {
            var images = new DataAccess().GetImagesByDirectory(code);
            return Json(images, JsonRequestBehavior.AllowGet);
        }
    }
}