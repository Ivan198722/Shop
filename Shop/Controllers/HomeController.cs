using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAdminAllProducts _adminAllProducts;
        private readonly IAllProducts _AllProducts;



        public HomeController(IAdminAllProducts adminRepository, IAllProducts allProducts) : base(adminRepository)
        {
            _adminAllProducts = adminRepository;
            _AllProducts = allProducts;
        }

        public async Task<IActionResult> Index()
        {
            var faivoriteProduct = await _AllProducts.GetProductDetailsAsync();

            var viewModel = new HomeViewModel
            { Products = faivoriteProduct };

            return View(viewModel);
        }
    }
}
