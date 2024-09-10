using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAdminAllProducts _adminAllProducts;
        private readonly IAllProducts _AllProducts;



        public HomeController(IAdminAllProducts adminRepository, IAllProducts allProducts, IAllShopCart allShopCart) : base(adminRepository, allShopCart)
        {
            _adminAllProducts = adminRepository;
            _AllProducts = allProducts;
        }

        public async Task<IActionResult> Index(string query)
        {
            var faivoriteProduct = await _AllProducts.GetProductDetailsAsync();
            if(query != null)
            {
                faivoriteProduct = await _AllProducts.Search(query);
            }

            var viewModel = new HomeViewModel
            { Products = faivoriteProduct };

            return View(viewModel);
        }

        public async Task<IActionResult> Search(string query)
        {
            return RedirectToAction("Index", new { query });
        }
        
    }
}
