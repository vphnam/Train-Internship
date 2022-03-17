using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainInternship.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
    }
}
