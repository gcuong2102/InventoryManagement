using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.Models.EF
{
    public class ExportDataViewModel
    {
        public List<Product> ListProducts { get; set; }
        public List<Customer> ListCustomers { get; set; }
        public List<string> NameCustomer { get; set; }
        public List<string> NameProduct { get; set; }
        public object NameCodeProduct { get; set; }
    }
}