using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class ProductViewModel
    {
        public IEnumerable<QualitySouvenirs.Models.Product> Products { get; set; }
        public IEnumerable<QualitySouvenirs.Models.Category> Categories { get; set; }
        public IEnumerable<QualitySouvenirs.Models.Supplier> Suppliers { get; set; }
        public int DisplayCategory { get; set; }
        public int DisplayItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}