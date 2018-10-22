using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Total GST")]
        public decimal GST { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        public int TotalCount { get; set; }
    }
}