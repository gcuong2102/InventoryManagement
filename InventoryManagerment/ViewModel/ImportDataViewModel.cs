using InventoryManagerment.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerment.ViewModel
{
    public class ImportDataViewModel
    {
        public List<Product> ListProducts { get; set; }
        public List<Supplier> ListSupplier { get; set; }
        public List<string> NameSupplier { get; set; }
        public List<string> NameProduct { get; set; }
        public object NameCodeProduct { get; set; }
    }
}