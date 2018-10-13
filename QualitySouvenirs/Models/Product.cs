using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QualitySouvenirs.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int ItemID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string FilePath { get; set; }

        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderLines { get; set; }
    }
}