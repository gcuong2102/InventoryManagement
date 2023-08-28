using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.ViewModel
{
    public class BillAutoComplete
    {
        public List<string> NameCustomer { get; set; } = new List<string>();
        public List<string> NameProduct { get; set; }
    }
}