using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllShopCart
    {

        int QuantityProduct(int productId);
        void AddToCart(Product product);
        IEnumerable<ShopCartItem> GetShopCartItems();
        Task<decimal> TotalPrice();
        int TotalItemsCount();
        Task RemoveFromCart(int productId);
        Task UpdateQuantity(int productId, int newQuantity);
        Task<IEnumerable<Product>> AllProducts();
        Task<int> TotalItemsCountAsync();
        Task<IEnumerable<ShopCartItem>> GetShopCartItemsAsync();
    }
}
