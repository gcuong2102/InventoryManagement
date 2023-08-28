using InventoryManagerment.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerment.Models.List
{
    public class ListDataImport
    {
        public List<Import> ListImports { get; set; }
        public List<Supplier> ListSuppliers { get; set; }
        public List<Product> ListProducts { get; set;}
        public List<Package> ListPackages { get; set; }
        public List<string> NameProducts { get; set; }
        public List<string> NameSuppliers { get; set; }
    }
}