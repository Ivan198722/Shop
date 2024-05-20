using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.Repository;
using Shop.ViewModels;


namespace Shop.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminAllProducts _adminAllProducts;

        

        public AdminController( IAdminAllProducts adminRepository):base(adminRepository)
        {
            _adminAllProducts = adminRepository;
        }

        public ActionResult Index()
        {
            
            return View();
        }

      
        public async Task<IActionResult> ProductList(int categoryId)
        {
            var products = await _adminAllProducts.GetProductOfCategoryAsync(categoryId);

            var viewModel = new AdminViewModel { Products = products };

            return View(viewModel);
        }

        public async Task <IActionResult> addProduct(int categoryId, string manufacturer, string name, ushort price)
        {
           await _adminAllProducts.AddProductAsync(categoryId, manufacturer, name, price);
            return RedirectToAction("ProductList", new { categoryId = categoryId });

        }
    }
}
