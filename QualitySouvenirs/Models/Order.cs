using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualitySouvenirs.Models
{
    public enum OrderStatus
    {
        Waiting, Shipped, Cancelled
    }

    public class Order
    {
        [Display(Name = "Order Number")]
        public int OrderID { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Total GST")]
        public decimal GST { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}