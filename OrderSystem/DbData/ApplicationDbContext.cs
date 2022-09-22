using Microsoft.EntityFrameworkCore;
using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.DbData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>()
             .HasKey(op => new { op.OrderId, op.ProductId });

            builder.Entity<OrderProduct>()
                .HasOne(b => b.Order)
                .WithMany(op => op.OrderProduct)
                .HasForeignKey(x=>x.OrderId);

            builder.Entity<OrderProduct>()
                .HasOne(b => b.Product)
                .WithMany(op => op.OrderProduct)
                .HasForeignKey(x=>x.ProductId);


            base.OnModelCreating(builder);

        }
    }
}
