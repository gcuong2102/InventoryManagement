using System;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagerment.Models.WINFORMS
{
    public class BANHANGCONTEXT : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'InventoryManagerment.Models.WINFORMS.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public BANHANGCONTEXT()
            : base("name=BANHANGCONTEXT")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<NOTES> NOTEs { get; set; }
        public virtual DbSet<HOADONBAN> HOADONBANs { get; set; }
        public virtual DbSet<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public virtual DbSet<invoice_receivce> Invoice_Receivces { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}