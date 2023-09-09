using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace InventoryManagerment.Models.EF
{
    [Table("Location")]
    public class location
    {
        [Key]
        public long id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string description { get; set; }
        public string username { get; set; }
        public DateTime created_date { get; set; }
        public string customername { get; set; }
        public string code { get; set; }
    }
}