using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainInternship.Models
{
    public class PurchaseOrderLine
    {
        [Key]
        public int PartNo { get; set; }
        [ForeignKey("PurchaseOrder")]
        public int OrderNo { get; set; }
        public string PartDescription { get; set; }
        public string Manufacturer { get; set; }
        public int QuantityOrder { get; set; }
        public float BuyPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string MeMo { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

    }
}
