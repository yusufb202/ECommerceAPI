using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("SqlConnection",
                    b => b.MigrationsAssembly("Repository"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Default" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 4, Name = "T-Shirt 1", Description = "Description 1", Price = 11, Stock = 21, CategoryId = 5 },
                new Product { Id = 5, Name = "T-Shirt 2", Description = "Description 2", Price = 12, Stock = 22, CategoryId = 5 },
                new Product { Id = 6, Name = "T-Shirt 3", Description = "Description 3", Price = 13, Stock = 23, CategoryId = 5 },
                new Product { Id = 7, Name = "T-Shirt 4", Description = "Description 4", Price = 14, Stock = 24, CategoryId = 5 },
                new Product { Id = 8, Name = "T-Shirt 5", Description = "Description 5", Price = 15, Stock = 25, CategoryId = 5 },
                new Product { Id = 9, Name = "T-Shirt 6", Description = "Description 6", Price = 16, Stock = 26, CategoryId = 5 },
                new Product { Id = 10, Name = "T-Shirt 7", Description = "Description 7", Price = 17, Stock = 27, CategoryId = 5 },
                new Product { Id = 11, Name = "T-Shirt 8", Description = "Description 8", Price = 18, Stock = 28, CategoryId = 5 },
                new Product { Id = 12, Name = "T-Shirt 9", Description = "Description 9", Price = 19, Stock = 29, CategoryId = 5 }
                );

            modelBuilder.Entity<Category>()
             .HasMany(c => c.Products)
             .WithOne(p => p.Category)
             .HasForeignKey(p => p.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Id)
                .ValueGeneratedOnAdd();
        }

    }
}
