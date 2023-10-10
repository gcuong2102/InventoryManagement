using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.ViewModel
{
    public class HomeViewModel
    {
        public double? sosanhdoanhthutheongay { get; set; }
        public double? sosanhchitieutheongay { get;set; }
        public int soluongphieuxuathomnay { get;set; }
        public int soluongphieuxuatchuaduyethomnay { get; set; }
        public int soluongphieuxuathomqua { get; set; }
        public int soluongphieuxuatchuaduyethomqua { get; set; }
        public int soluongphieunhaphomnay { get;set; }
        public int soluongphieunhaphomqua { get; set; }
        public int soluongphieuxuattrongtuan { get; set; }
        public int soluongphieunhaptrongtuan { get; set; }
        public double? doanhthuhomnay { get; set; }
        public double? tongchihomnay { get; set; }
        public double? doanhthuhomqua { get; set; }
        public double? tongchihomqua { get; set; }
        public double? doanhthutuannay { get; set; }
        public double? tongchituannay { get; set; }
        public double? doanhthutheothang { get; set; }
        public double? chitieutheothang { get; set; }
        public double? loinhuantheothang { get; set; }
    }
}