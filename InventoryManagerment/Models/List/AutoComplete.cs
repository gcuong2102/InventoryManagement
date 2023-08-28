using InventoryManagerment.Models.EF;
using InventoryManagerment.Models.ModelProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerment.Models.List
{
    public class AutoComplete
    {
        public List<ProductAutoComplete> ListProducts { get; set; }
        public List<Customer> ListCustomers { get; set; }
        public List<Product> DataProducts { get; set; }
        public List<DataProduct> DataDataProducts { get; set; }
    }
}