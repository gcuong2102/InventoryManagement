using InventoryManagerment.Common;
using InventoryManagerment.Models.ModelProduct;
using InventoryManagerment.Models.WINFORMS;
using InventoryManagerment.ViewModel;
using MySqlX.XDevAPI.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerment
{
    public class DataAccess2
    {
        BANHANGCONTEXT db;
        public DataAccess2()
        {
            db = new BANHANGCONTEXT();
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        //Chức năng sổ xuất và hóa đơn bán
        public IEnumerable<NOTES> ListAllNotesToPagedList(string searchString,string note,DateTime? dateNote,string status,int page, int pageSize)
        {
            string date;
            if (dateNote.HasValue)
            {
                date = dateNote.Value.ToString("'Ngày' dd 'Tháng' MM 'Năm' yyyy");
            }
            else
            {
                date = "";
            }
            if (!string.IsNullOrEmpty(note))
            {
                note = note.Trim();
            }
            else
            {
                note = "";
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
            }
            else
            {
                searchString = "";
            }
            if (!string.IsNullOrEmpty(status))
            {
                if(status == "Đã Thanh Toán")
                {
                    status = "Đã";
                }
                if(status == "Chưa Thanh Toán (Có HĐ)")
                {
                    status = "Có";
                }
                if(status == "Chưa Thanh Toán (Không HĐ)")
                {
                    status = "Không";
                }
                if(status == "Tất cả")
                {
                    status = "";
                }
            }
            else
            {
                status = "";
            }
            var result = from ghichu in db.NOTEs
                         where ghichu.CUSTOMER.Contains(searchString) && ghichu.NOTE.Contains(note) && ghichu.TIME.Contains(date) && ghichu.STATUS.Contains(status)
                         orderby string.Concat(ghichu.TIME.Substring(21, 4), ghichu.TIME.Substring(14, 2), ghichu.TIME.Substring(5, 2), ghichu.TIME.Substring(28, 2), ghichu.TIME.Substring(35, 2), ghichu.TIME.Substring(43, 2)) descending
                         select ghichu;
            return result.ToPagedList(page, pageSize);
        }
        public string GetNote(string code)
        {
            return db.NOTEs.Where(x => x.ID == code).FirstOrDefault().NOTE;
        }
        //public IEnumerable<BillViewModel> ListAllHoaDonToPagedList(string searchString, string nameProduct,string totalPrice,DateTime? dateBill, int page, int pageSize)
        //{
        //    string date;
        //    double price;
        //    if (dateBill.HasValue)
        //    {
        //        date = dateBill.Value.ToString("'Ngày' dd 'Tháng' MM 'Năm' yyyy");
        //    }
        //    else
        //    {
        //        date = "Ngày";
        //    }
        //    if(!string.IsNullOrEmpty(totalPrice))
        //    {
        //        price = Convert.ToDouble(Functions.RemoveCharacters(totalPrice));
        //    }
        //    else
        //    {
        //        price = double.MaxValue;
        //    }
        //    if (!string.IsNullOrEmpty(nameProduct))
        //    {
        //        nameProduct = nameProduct.Trim();
        //    }
        //    else
        //    {
        //        nameProduct = ""; 
        //    }
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        searchString = searchString.Trim();
        //    }
        //    else
        //    {
        //        searchString = "";
        //    }
        //    var ketqua = (from bill in db.HOADONBANs
        //                  join detail in db.CHITIETHOADONs on bill.MAHOADON equals detail.MAHOADON
        //                  where bill.TENKHACHHANG.Contains(searchString) && detail.TENSANPHAM.Contains(nameProduct) && bill.NGAYBAN.Contains(date) && bill.TONGTIEN <= price
        //                  select new BillViewModel()
        //                  {
        //                      MAHOADON = bill.MAHOADON,
        //                      TENSANPHAM = detail.TENSANPHAM,
        //                      STT = bill.STT,
        //                      DEPT = bill.DEPT,
        //                      NGAYBAN = bill.NGAYBAN,
        //                      TENKHACHHANG = bill.TENKHACHHANG,
        //                      TONGTIEN = bill.TONGTIEN
        //                  }).Distinct().ToList();
        //    return ketqua.OrderByDescending(x=>string.Concat(x.NGAYBAN.Substring(21, 4), x.NGAYBAN.Substring(14, 2), x.NGAYBAN.Substring(5, 2), x.NGAYBAN.Substring(28, 2), x.NGAYBAN.Substring(35, 2), x.NGAYBAN.Substring(43, 2))).ToPagedList(page, pageSize);          
        //}
        public List<BillModel> GetListBill(string code)
        {
            List<BillModel> listBill = new List<BillModel>();
            var hoadonban = db.HOADONBANs.Where(x => x.MAHOADON == code).FirstOrDefault();
            var danhsachchitiet = db.CHITIETHOADONs.Where(x => x.MAHOADON == code).ToList();
            foreach(var item in danhsachchitiet)
            {
                var model = new BillModel();
                model.DEPT = hoadonban.DEPT;
                model.DONVITINH = item.DONVITINH;
                model.GIADONVI = item.GIADONVI;
                model.MAHOADON = item.MAHOADON;
                model.NGAYBAN = hoadonban.NGAYBAN;
                model.SOLUONG = item.SOLUONG;
                model.STT = hoadonban.STT;
                model.THANHTIEN = item.THANHTIEN;
                model.TENKHACHHANG = hoadonban.TENKHACHHANG;
                model.TENSANPHAM = item.TENSANPHAM;
                model.TONGTIEN = hoadonban.TONGTIEN;
                listBill.Add(model);
            }
            return listBill;
        }
        public List<string> GetNameCustomer()
        {
            return db.NOTEs.Select(x => x.CUSTOMER).Distinct().ToList();
        }
        public BillAutoComplete GetNameProduct()
        {
            var result = new BillAutoComplete()
            {
                NameCustomer = db.NOTEs.Select(x => x.CUSTOMER).Distinct().ToList(),
                NameProduct = db.CHITIETHOADONs.Select(x => x.TENSANPHAM).Distinct().ToList(),
            };
            return result;
        }
        public IEnumerable<BillViewModel> ListAllHoaDonToPagedList(string searchString, string nameProduct, string totalPrice, DateTime? dateBill, int page, int pageSize)
        {
            string date = FormatDate(dateBill);
            double price = ParsePrice(totalPrice);
            nameProduct = CleanString(nameProduct);
            searchString = CleanString(searchString);

            var query = from bill in db.HOADONBANs
                        join detail in db.CHITIETHOADONs on bill.MAHOADON equals detail.MAHOADON
                        where bill.TENKHACHHANG.Contains(searchString)
                              && detail.TENSANPHAM.Contains(nameProduct)
                              && bill.NGAYBAN.Contains(date)
                              && bill.TONGTIEN <= price
                        select new
                        {
                            Bill = bill,
                            Detail = detail
                        };

            var groupedQuery = from q in query
                               group q by q.Bill.MAHOADON into grouped
                               let firstItem = grouped.FirstOrDefault() // Use let to get the first item
                               where firstItem != null // Ensure that the first item isn't null
                               select new BillViewModel()
                               {
                                   MAHOADON = grouped.Key,
                                   TENSANPHAM = firstItem.Detail.TENSANPHAM,
                                   STT = firstItem.Bill.STT,
                                   DEPT = firstItem.Bill.DEPT,
                                   NGAYBAN = firstItem.Bill.NGAYBAN,
                                   TENKHACHHANG = firstItem.Bill.TENKHACHHANG,
                                   TONGTIEN = firstItem.Bill.TONGTIEN
                               };


            var ketqua = groupedQuery
                         .ToList() // Thực thi truy vấn và lấy dữ liệu
                         .OrderByDescending(x => ParseNgayBanToSortableString(x.NGAYBAN))
                         .ToPagedList(page, pageSize);

            return ketqua;
        }
        private string FormatDate(DateTime? dateBill)
        {
            return dateBill?.ToString("'Ngày' dd 'Tháng' MM 'Năm' yyyy") ?? "Ngày";
        }

        private double ParsePrice(string totalPrice)
        {
            return string.IsNullOrWhiteSpace(totalPrice) ? double.MaxValue : Convert.ToDouble(Functions.RemoveCharacters(totalPrice));
        }

        private string CleanString(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "" : input.Trim();
        }

        private string ParseNgayBanToSortableString(string ngayBan)
        {
            return string.Concat(
                ngayBan.Substring(21, 4),
                ngayBan.Substring(14, 2),
                ngayBan.Substring(5, 2),
                ngayBan.Substring(28, 2),
                ngayBan.Substring(35, 2),
                ngayBan.Substring(43, 2));
        }

    }
}