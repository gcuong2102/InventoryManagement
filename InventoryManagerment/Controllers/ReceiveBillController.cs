using InventoryManagerment.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using ImageMagick;
using MySqlX.XDevAPI.Common;
using InventoryManagerment.Models.EF;
using System.Web.Services.Description;
using InventoryManagerment.Models.WINFORMS;

namespace InventoryManagerment.Controllers
{
    public class ReceiveBillController : BaseController
    {
        // GET: ReceiveBill
        [HttpGet]
        public ActionResult Index(DateTime? time,string customerName, string staffName, string note, string address ,int page = 1, int pageSize = 15)
        {
            var user = GetUser();
            if(user.RoleID == 1)
            {
                SetViewBag(time, customerName, staffName, note, address, pageSize);
                var model = new DataAccess().GetDataReceiveBill(time, customerName, staffName, note, address, page, pageSize);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
            
        }
        public void SetViewBag(DateTime? time, string customerName, string staffName, string note, string address, int pageSize)
        {
            if (time.HasValue)
            {
                ViewBag.time = time;
            }
            if (pageSize != 15)
            {
                ViewBag.pageSize = pageSize;
            }
            if (!string.IsNullOrEmpty(customerName))
            {
                ViewBag.customerName = customerName;
            }
            if (!string.IsNullOrEmpty(staffName))
            {
                ViewBag.staffName = staffName;
            }
            if (!string.IsNullOrEmpty(note))
            {
                ViewBag.note = note;
            }
            if (!string.IsNullOrEmpty(address))
            {
                ViewBag.address = address;
            }

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
                string uniqueFolderName = ConvertFolderName(customerName.Trim()) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string folderPath = Path.Combine(@"\ReceivedBill", uniqueFolderName);
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
                        var fileName = ConvertFolderName(customerName.Trim()) + Functions.GenerateUniqueCode() + ".webp"; // Lưu với đuôi .webp
                        var path = Path.Combine(folderPath, fileName); // Lưu vào thư mục duy nhất

                        using (var imageStream = new MemoryStream())
                        {
                            file.InputStream.CopyTo(imageStream);
                            imageStream.Position = 0;
                            using (var image = new MagickImage(imageStream))
                            {
                                image.AutoOrient(); // Tự động xoay hình ảnh dựa trên thông tin EXIF
                                image.Format = MagickFormat.WebP;
                                image.Quality = 78;
                                image.Write(path);
                            }
                        }
                        listUrl.Add(path);
                    }
                }
                var username = GetUserName();
                var result = new DataAccess().UploadImages(listUrl, txtTime, customerName, note, GetUserName(), latitude, longitude, address);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public JsonResult GetAutoComplete()
        {
            var result = new DataAccess().GetListAutoCompleteForReceiveBill();
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string code)
        {
            var result = new DataAccess().DeleteImage(code);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet] 
        public JsonResult GetNextData(DateTime? time, string customerName, string staffName, string note, string address, int page, int pageSize) 
        {
            var result = new DataAccess().GetReplacementData(page, pageSize,time, customerName, staffName, note, address);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetMultiNextData(DateTime? time, string customerName, string staffName, string note, string address, int page, int pageSize)
        {
            var result = new DataAccess().GetReplacementData(page, pageSize, time, customerName, staffName, note, address);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMultiple(List<string> listCode)
        {
            var result = new DataAccess().DeleteMultipleFolder(listCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditReceiveBill(IEnumerable<HttpPostedFileBase> files,string folderName, string code, string time, string customername, string address, string note, string staff)
        {
            try
            {
                if (files != null)
                {
                    var listUrl = new List<string>();
                    foreach (var file in files) 
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = ConvertFolderName(customername.Trim()) + Functions.GenerateUniqueCode() + ".webp"; // Lưu với đuôi .webp
                            var path = Path.Combine(folderName.Replace('/','\\'), fileName); // Lưu vào thư mục duy nhất

                            using (var imageStream = new MemoryStream())
                            {
                                file.InputStream.CopyTo(imageStream);
                                imageStream.Position = 0;
                                using (var image = new MagickImage(imageStream))
                                {
                                    image.AutoOrient(); // Tự động xoay hình ảnh dựa trên thông tin EXIF
                                    image.Format = MagickFormat.WebP;
                                    image.Quality = 78;
                                    image.Write(path);
                                }
                            }
                            listUrl.Add(path);
                        }
                    }
                    new DataAccess().UpdateImages(listUrl, code, time, customername, note, staff, address);             
                }
                else
                {
                    new DataAccess().UpdateInformationReceiveBill(code, time, customername, staff, note, address);
                }
                return Json(new {result = true,error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { result = false,error =ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult DeleteImage(string fileName, string code)
        {
            try
            {
                var db = new InventoryDbContext();
                string namePic = HttpUtility.UrlDecode(fileName);
                var result = db.ReceiveBill.Where(x => x.Code == code && x.Url_Image.Contains(namePic)).FirstOrDefault();
                if (result == null)
                {
                    return Json(new { result = false, message = "Không tìm thấy ảnh này" });
                }
                System.IO.File.Delete(result.Url_Image);
                db.ReceiveBill.Remove(result);
                db.SaveChanges();
                return Json(new { result = true, message = "Xóa ảnh thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { result = false, message = "Xóa ảnh thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public JsonResult CheckNameStaff(string nameStaff)
        {
            var db = new InventoryDbContext();
            var staff = db.Users.Where(x => x.Name == nameStaff).FirstOrDefault();
            if (staff == null)
            {
                return Json(new { result = false, message = "Không tồn tại tên nhân viên này, vui lòng kiểm tra lại" },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetNameStaffCustomer()
        {
            var db = new InventoryDbContext();
            var staff = db.Users.Select(x => x.Name).ToList();
            var customer = new DataAccess2().GetNameCustomer();
            return Json(new { staff = staff, customer = customer },JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetDataInvoice(string time, string receiveCode)
        {
            var data = new DataAccess2().GetInvoiceByTime(time, receiveCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MergeInvoice(string[] listMahoadon, string image)
        {
            var result = new DataAccess2().MergeInvoice(listMahoadon, image);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private string ConvertFolderName(string fileName)
        {
            return fileName.Replace('\\', '.').Replace('/', '.');
        }
        private void SaveCompressedImage(Stream sourceStream, string outputPath, long quality)
        {
            using (var image = Image.FromStream(sourceStream))
            {
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                var jpegCodec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                if (jpegCodec == null) return; // No jpeg codec available
                image.Save(outputPath, jpegCodec, encoderParameters);
            }
        }
    }
}