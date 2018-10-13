using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QualitySouvenirs.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public string ItemCategory { get; set; }
        public string ItemName { get; set; }
        public string ImageName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string UserID { get; set; }

        public const string sessionKey = "cartID";
    }
}