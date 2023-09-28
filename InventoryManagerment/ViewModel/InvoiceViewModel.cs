using InventoryManagerment.Models.WINFORMS;
using System.Collections.Generic;

public class InvoiceViewModel
{
    public string MAHOADON { get; set; }
    public string NGAYBAN { get; set; }
    public string TENKHACHHANG { get; set; }
    public double TONGTIEN { get; set; }
    public bool LINKED { get; set; }
    public List<CHITIETHOADON> Details { get; set; } 
}