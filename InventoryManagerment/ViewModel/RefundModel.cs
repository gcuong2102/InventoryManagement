﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerment.ViewModel
{
    public class RefundModel
    {
        public string RefundCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string PackageName { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string CustomerName { get; set; }
        public string UnitName { get; set; }
        public string Staff { get; set; }
        public string Time { get; set; }
        public string Note { get; set; }
        public decimal Total { get; set; }
    }
}