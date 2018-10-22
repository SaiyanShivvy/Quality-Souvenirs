using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<CartItem>().ToTable("CartItem");
            builder.Entity<OrderItem>().ToTable("OrderDetail");
            builder.Entity<OrderItem>().HasOne(p => p.Order).WithMany(o => o.OrderItems).OnDelete(DeleteBehavior.Cascade);
        }
    }
}