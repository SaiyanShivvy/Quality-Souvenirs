using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualitySouvenirs.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public decimal OrderItemPrice { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}