using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Controllers
{
    public class BaseAdminController : Controller, IAsyncActionFilter
    {
        private readonly IAdminAllProducts _adminRepository;
        private readonly IAllSale _allSale;

        public BaseAdminController(IAdminAllProducts adminRepository, IAllSale allSale)
        {
            _adminRepository = adminRepository;
            _allSale = allSale;
        }



        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var categories = await GetAllCategoriesAsync();

            
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.Menu = categories;
            }

            ViewBag.CountActiveOrders = await GetCountActiveOrders();
            

            await next();
        }


        public async Task<IEnumerable<ProductInfo>> GetAllCategoriesAsync()
        {
            var allCategories = await _adminRepository.GetAllCategoriesAsync();

            return allCategories;
        }

       public async Task<int> GetCountActiveOrders()
        {
            return await _allSale.GetCountActiveOrders();
        }
    }
}
