using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products  { get; set; }

        public DbSet<Images> Images { get; set; }

        public DbSet<ProductHighlights> ProductHighlights { get; set; }

        public DbSet<ProductProperties> ProductProperties { get; set; }

        public DbSet<ShopCartItem> ShopCartItems { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<FinishedOrder> FinishedOrders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.addDate)
                .HasColumnType("date"); 
        }
    }
}
