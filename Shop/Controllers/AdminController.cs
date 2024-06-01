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

      
        public async Task<IActionResult> ProductList(int categoryId, string[]? selectedModels, decimal? priceFrom, decimal? priceTo, DateTime? fromDate, DateTime? toDate, string query)
        {
            var products = await _adminAllProducts.GetProductOfCategoryAsyncSortDate(categoryId);
          
            if (selectedModels.Length>0 || priceFrom != null || priceTo != null || fromDate != null || toDate != null)
            {
               products = await _adminAllProducts.FiterProductsAsync(categoryId, selectedModels, priceFrom, priceTo, fromDate, toDate);
                
            }

            if(!string.IsNullOrEmpty(query))
            {
                products = await _adminAllProducts.Search(categoryId, query);
            }
            var productsList = await _adminAllProducts.MarkManufacturersAsync(categoryId, selectedModels);

            var viewModel = new AdminViewModel 
            {
                Products = products,

                UniqueProd=productsList

            };

            return View(viewModel);
        }

        public async Task <IActionResult> addProduct(int categoryId, string manufacturer, string name, decimal price)
        {
           await _adminAllProducts.AddProductAsync(categoryId, manufacturer, name, price);
            return RedirectToAction("ProductList", new { categoryId = categoryId });

        }

        public async Task<IActionResult> Filter(int categoryId, string[]? selectedModels, decimal? priceFrom, decimal? priceTo, DateTime? fromDate, DateTime? toDate)
        {
            if (selectedModels != null || priceFrom != null || priceTo != null|| fromDate!= null|| toDate!=null)
            {
              
                return  RedirectToAction("ProductList", new {categoryId=categoryId,  selectedModels, priceFrom, priceTo, fromDate, toDate });
            }
            else
            {
               
                return RedirectToAction("ProductList", new {categoryId=categoryId});
            }
        }

        public async Task<IActionResult> Search(int categoryId, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return RedirectToAction("ProductList", new { categoryId = categoryId, query });
            }
            else
            {
                return RedirectToAction("ProductList", new { categoryId = categoryId });
            }
           
        }
    }
}
