using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class ShopCartController : BaseController
    {
        private readonly IAllShopCart _allShopCart;

        private readonly ShopCart _shopCart;

        public ShopCartController(IAllShopCart allShopCart, ShopCart shopCart, IAdminAllProducts adminAllProducts):base(adminAllProducts, allShopCart)
        {
            _allShopCart = allShopCart;

            _shopCart = shopCart;
        }


        public async Task<IActionResult> IndexShopCart()
        {
            ViewBag.Title = "Koszyk";
            var items = await _allShopCart.GetShopCartItemsAsync();
            _shopCart.ListShopItems = items;

            var totalPrice = await _allShopCart.TotalPrice();

            var modelView = new ShopCartViewModel
            {
                shopCart = _shopCart,
                totalPrice = totalPrice,
            };

            return View(modelView);
        }

        public async Task<IActionResult> addToCart(int productId, string returnUrl, int page, int categoryId)
        {
            var products = await _allShopCart.AllProducts();
            var product = products.FirstOrDefault(p=>p.Id == productId);


            if (product != null)
            {
                
                var existingProductItems = await _allShopCart.GetShopCartItemsAsync();
                var existingProductItem = existingProductItems.FirstOrDefault(p=>p.product.Id==productId);


                if (existingProductItem != null)
                {
                     await UpdateCartItem(productId, "increment");
                }
                else
                {

                    _allShopCart.AddToCart(product);
                    

                }
            }

            return Redirect($"{returnUrl}?categoryId={categoryId}&&page={page}");
        }

        public async Task<IActionResult> UpdateCartItem(int productId, string action)
        {
            var cartItems = await _allShopCart.GetShopCartItemsAsync();
            var cartItem = cartItems.FirstOrDefault(c => c.product.Id == productId);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (action == "increment")
            {
                await _allShopCart.UpdateQuantity(productId, cartItem.quantity + 1);
            }
            else if (action == "decrement")
            {
                if (cartItem.quantity > 1)
                {
                    await _allShopCart.UpdateQuantity(productId, cartItem.quantity - 1);
                }
                else
                {
                    await _allShopCart.RemoveFromCart(productId);
                }
            }

            return RedirectToAction("IndexShopCart");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _allShopCart.RemoveFromCart(productId);
            return RedirectToAction("IndexShopCart");
        }
    }
}
