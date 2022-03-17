using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainInternship.Models;

namespace TrainInternship.Context
{
    public class ShopContext: DbContext
    {
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-RV032LIG\MAYCHU;Initial Catalog=ShopDB;Trusted_Connection=True;");
            //Data Source=LAPTOP-RV032LIG\MAYCHU;Initial Catalog=Shop
        }
    }
}
