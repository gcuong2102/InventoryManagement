using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.Models
{
    public class DirectoryImage
    {
        public string DirectoryName { get; set; }
        public string FirstImage { get; set; }
        public long UserID { get; set; }
        public DateTime Time { get; set; }
        public string NameCustomer { get; set; }
        public string code { get; set; }
    }
}