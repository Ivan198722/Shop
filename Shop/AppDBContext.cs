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

        public DbSet<Image> Images { get; set; }

        public DbSet<ProductHighlights> ProductHighlights { get; set; }

        public DbSet<CellphoneProperties> CellphoneProperties { get; set; }

        public DbSet<TVProperties> TVProperties { get; set; }

        public DbSet<PhotoProperties> PhotoProperties { get; set; }

        public DbSet<ShopCartItem> ShopCartItems { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<FinishedOrder> FinishedOrders { get; set; }

    }
}
