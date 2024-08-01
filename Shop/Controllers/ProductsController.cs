using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.ViewModels;
using static NuGet.Client.ManagedCodeConventions;

namespace Shop.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IAdminAllProducts _adminAllProducts;
        private readonly IAllProducts _AllProducts;

        public ProductsController(IAdminAllProducts adminRepository, IAllProducts allProducts) : base(adminRepository)
        {
            _adminAllProducts = adminRepository;
            _AllProducts = allProducts;
        }

        public async Task<IActionResult> List(int categoryId, int? page, string[] selectedModels, string[] selectedProperties,
            decimal? priceFrom, decimal? priceTo, string[] selectedHighlights)
        {

            var products = await _AllProducts.GetProductOfCategory(categoryId);

            if (selectedModels.Length > 0 || selectedProperties.Length > 0 || priceFrom!=null || priceTo!=null|| selectedHighlights.Length > 0)
            {
                products = await _AllProducts.FiterProductsAsync(categoryId, selectedModels, selectedProperties, priceFrom, priceTo, selectedHighlights);
            }


            var uniqueProd = await _adminAllProducts.MarkManufacturersAsync(categoryId, selectedModels);

            var uniqueProperty = await _AllProducts.GetProductProperties(categoryId,selectedModels, selectedProperties);

           


            int totalItems = products?.Count() ?? 0;
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var paginatedProducts = products?.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            if (paginatedProducts?.Any() == false && pageNumber > 1)
            {
                pageNumber--;
                paginatedProducts = products?.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.CurrentPage = pageNumber;
            

            var viewModel = new ProductsViewModel 
            {
                Products = paginatedProducts,
                TotalPages = totalPages,
                CurrentPage=pageNumber,
                CategoryId= categoryId,
                UniqueProd=uniqueProd,
                UniqueProperty=uniqueProperty,
                Highlights=products
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Filter(int categoryId, string[] selectedModels, string[] selectedProperties,
            decimal? priceFrom, decimal? priceTo, string[] selectedHighlights)
        {
           

                return RedirectToAction("List", new { categoryId = categoryId, selectedModels, selectedProperties,
                    priceFrom, priceTo, selectedHighlights });
          
        }
    }
}
