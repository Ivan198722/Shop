using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Interfaces;
using Shop.Models;
using Shop.Repository;

namespace Shop.Controllers
{
    public class BaseController : Controller, IAsyncActionFilter
    {
        private readonly IAdminAllProducts _adminRepository;
        private readonly IAllShopCart _allShopCart;

        public BaseController(IAdminAllProducts adminRepository, IAllShopCart allShopCart)
        {
            _adminRepository = adminRepository;
            _allShopCart = allShopCart;
        }

       

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          
            var categories = await GetAllCategoriesAsync();

            // Setting the value of ViewBag.CartImage for all views
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.Menu = categories;
            }


            ViewBag.ItemsCount = await GetCartItems();
           
            await next();
        }
       

        public async Task<IEnumerable<ProductInfo>> GetAllCategoriesAsync()
        {
            var allCategories = await _adminRepository.GetAllCategoriesAsync();

            return allCategories;
        }

        private async Task< int> GetCartItems()
        {
            var totalItemsCount = await _allShopCart.TotalItemsCountAsync();
           
            return  totalItemsCount;
        }
    }
}
