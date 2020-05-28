using System;
using System.Collections.Generic;
using HW_26.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_26.Models
{
    public class DateDb : DbContext
    {
        public DateDb(DbContextOptions<DateDb> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(e => e.List)
                .HasConversion(v => v.ToString(),
                v => (ProductList)Enum.Parse(typeof(ProductList), v));
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, List = ProductList.Fruits },
                new Category { CategoryId = 2, List = ProductList.Meaty },
                new Category { CategoryId = 3, List = ProductList.Milk });
            

            

            
        }
    }
    
}
