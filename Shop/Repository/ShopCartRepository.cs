using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
using Microsoft.Extensions.Logging;

namespace Shop.Repository
{
    public class ShopCartRepository:IAllShopCart

    {
        private readonly AppDBContext _dbContext;

        private readonly ShopCart _shopCart;

       

        public ShopCartRepository(AppDBContext appDBContext, ShopCart shopCart)
        {
            _dbContext = appDBContext;
            _shopCart = shopCart;
           

        }

        public static ShopCart GetCart(IServiceProvider services)
        {
            var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
            var session = httpContextAccessor.HttpContext?.Session;
            var context = services.GetRequiredService<AppDBContext>();

            string shopCartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };

        }

        

        public int QuantityProduct(int productId) => 
            _dbContext.Products.Where(c => c.Id == productId)
            .Select(q => q.quantity)
            .FirstOrDefault();

        public Images GetImageForPrduct(int productId)
        {
            return _dbContext.Images
                .Where(im => im.productId == productId && im.numberImgs == 1)
                .FirstOrDefault();
            
        }

        //public async Task AddToCart(Product product)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Starting AddToCart method.");

        //        var quantityProduct = await QuantityProduct(product.Id);
        //        _logger.LogInformation($"Quantity for Product ID: {product.Id} is {quantityProduct}");

        //        var image = await GetImageForPrduct(product.Id);
        //        _logger.LogInformation($"Image for Product ID: {product.Id}, Image found: {image != null}");


        public void AddToCart(Product product)
        {

            var quatityProduct =  QuantityProduct(product.Id);

            var image = GetImageForPrduct(product.Id);
           
            if (quatityProduct > 0)
            {
                var shopCartItem = new ShopCartItem
                {
                    ShopCartId = _shopCart.ShopCartId,
                    product = product,
                    quantity = 1,
                    pricePerUnit = product.price,
                    price = product.price,
                    images = image
                };

               _dbContext.ShopCartItems.Add(shopCartItem);

                _dbContext.SaveChanges();
            }
        }



        public IEnumerable<ShopCartItem>  GetShopCartItems()
        {
            return _dbContext.ShopCartItems.Where(c => c.ShopCartId == _shopCart.ShopCartId)
                .Include(s => s.product)
                .Include(im=>im.images)
                
                .ToList();
        }

        public async Task< IEnumerable<ShopCartItem>> GetShopCartItemsAsync()
        {
            return await _dbContext.ShopCartItems.Where(c => c.ShopCartId == _shopCart.ShopCartId)
                .Include(s => s.product)
                .Include(im => im.images)

                .ToListAsync();
        }

        public async Task<decimal> TotalPrice()
        {
            var item =  GetShopCartItems();
            return  item.Sum(item => item.product.price * item.quantity);
        }

        public int TotalItemsCount()
        {
            var items =  GetShopCartItems();
            return items.Sum(item => item.quantity);
        }
        public async Task< int> TotalItemsCountAsync()
        {
            var items = await GetShopCartItemsAsync();
            return items.Sum(item => item.quantity);
        }

        public async Task RemoveFromCart(int productId)
        {
            var cartItem = await _dbContext.ShopCartItems
                .FirstOrDefaultAsync(item => item.product.Id == productId && item.ShopCartId == _shopCart.ShopCartId);

            if (cartItem != null)
            {
                _dbContext.ShopCartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantity(int productId, int newQuantity)
        {
            var cartItem = await _dbContext.ShopCartItems
                .FirstOrDefaultAsync(item => item.product.Id == productId && item.ShopCartId == _shopCart.ShopCartId);

            if (cartItem != null)
            {
                var quantityProduct = QuantityProduct(productId);
                if (newQuantity <= quantityProduct) 
                {
                    cartItem.quantity = newQuantity;
                    cartItem.price = cartItem.product.price * newQuantity;
                    await _dbContext.SaveChangesAsync(); 
                }
            }
        }

       
        public async Task<IEnumerable<Product>> AllProducts() => await _dbContext.Products.ToListAsync();
    }
}
