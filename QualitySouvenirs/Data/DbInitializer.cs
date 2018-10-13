using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class DbInitializer
    {
        public static void Initialize(QualitySouvenirsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return; //Seeded DB
            }

            var categories = new Category[]
            {
                new Category {Name = "Pins"},
                new Category {Name = "Keychain"}
            };

            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier {Name = "Supplier A", MobilePhoneNumber = "021-123123", EmailAddress = "fakeEmail@gmail.com" }
            };

            foreach (var s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product {ItemName = "Pin 1", Description = "Description 1", Price = Convert.ToDecimal(149.95), FilePath = "item1.jpg", CategoryID = categories.Single( c => c.Name == "Pins").CategoryID, SupplierID = 1 },
                new Product {ItemName = "Pin 2", Description = "Description 2", Price = Convert.ToDecimal(249.95), FilePath = "item2.jpg", CategoryID = categories.Single( c => c.Name == "Pins").CategoryID, SupplierID = 1 }
            };

            foreach (var p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
    }
}