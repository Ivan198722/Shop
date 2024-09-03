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

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка формата даты для свойства addDate
            modelBuilder.Entity<Product>()
                .Property(p => p.addDate)
                .HasColumnType("date");

            // Настройка каскадного удаления для Images при удалении Product
            modelBuilder.Entity<Images>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.productId)
                .OnDelete(DeleteBehavior.Cascade); // Оставляем Cascade

            // Настройка каскадного удаления для ProductHighlights при удалении Product
            modelBuilder.Entity<ProductHighlights>()
                .HasOne(ph => ph.Product)
                .WithMany(p => p.Highlights)
                .HasForeignKey(ph => ph.productId)
                .OnDelete(DeleteBehavior.Cascade); // Оставляем Cascade

            // Настройка каскадного удаления для ProductProperties при удалении Product
            modelBuilder.Entity<ProductProperties>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProductProperties)
                .HasForeignKey(pp => pp.productId)
                .OnDelete(DeleteBehavior.Cascade); // Оставляем Cascade

            // Настройка каскадного удаления для ProductProperties при удалении Category
            modelBuilder.Entity<ProductProperties>()
                .HasOne(pp => pp.Category)
                .WithMany(c => c.productProperties)
                .HasForeignKey(pp => pp.categoryId)
                .OnDelete(DeleteBehavior.Restrict); // Изменено на Restrict чтобы избежать циклов
        }
    }
}
