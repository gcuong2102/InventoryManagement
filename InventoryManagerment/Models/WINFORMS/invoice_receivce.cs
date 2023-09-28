using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.Models.WINFORMS
{
    [Table("invoice_receivce")]
    public partial class invoice_receivce
    {
        [Key]
        public long id { get; set; }
        public string invoicecode { get; set; }
        public string receivecode { get; set; }
        public DateTime created_date {get;set; }
        public string folder_url { get; set; }
    }
}