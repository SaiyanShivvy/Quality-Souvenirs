using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class CartItem
    {
        public string CartID { get; set; }
        public int CartItemID { get; set; }
        public int ItemCount { get; set; }
        public DateTime DateCreated { get; set; }
        public Product Product;
    }
}