using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainInternship.Context;

namespace TrainInternship.Models
{
    public class PurchaseOrder
    {
        ShopContext _myContext = new ShopContext();
        [Key]
        public int OrderNo { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierNo { get; set; }
        public string StockSite { get; set; }
        public string StockName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public bool SentMail { get; set; }
        public bool Status { get; set; }    
        public List<PurchaseOrderLine> PurchaseOrderLines {
            get => _myContext.PurchaseOrderLine.Where(n => n.OrderNo == this.OrderNo).ToList();
            set {; }
        }
        public Supplier Supplier
        {
            get => _myContext.Supplier.Find(this.SupplierNo);
            set {; }
        }
    }
}
