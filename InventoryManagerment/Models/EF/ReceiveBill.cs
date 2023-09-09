using InventoryManagerment.Models.WINFORMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.Models.EF
{
    [Table("ReceiveBill")]
    public class ReceiveBill
    {
        public long ID { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public DateTime Time { get;set; }
        public string Url_Image { get; set; }
        public long UserID { get; set; }
        public string Code { get; set; }
    }
}