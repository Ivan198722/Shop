using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.Repository;
using Shop.ViewModels;


namespace Shop.Controllers
{
    public class AdminController : BaseController
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

        public async Task<ActionResult> CategorySet()
        { 
            var category= await _adminAllProducts.GetAllCategoriesAsync();

            var viewModels = new AdminViewModel
            {
                CategoryInfo=category
            };
            return View(viewModels);
        }

        public async Task<ActionResult> AddProperty(int categoryId, string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                await _adminAllProducts.AddProperty(categoryId, propertyName);
                return RedirectToAction("CategorySet");
            }
            return RedirectToAction("CategorySet");
        }

        public async Task<ActionResult> EditProperty(int categoryId, string propertyName, string newPropertyName)
        {
            await _adminAllProducts.EditProperty(categoryId, propertyName, newPropertyName);

            return RedirectToAction("CategorySet");
        }

        public async Task<ActionResult> EditDescription(int categoryId, string newDescription)
        {
            await _adminAllProducts.EditDescription(categoryId, newDescription);

            return RedirectToAction("CategorySet");
        }

        public async Task<ActionResult> AddCategory(string categoryName)
        {
            await _adminAllProducts.AddCategory(categoryName);
            return RedirectToAction("CategorySet");
        }

        public async Task<ActionResult> EditCategory(int categoryId, string newCategoryName)
        {
            await _adminAllProducts.EditCategory(categoryId, newCategoryName);
            return RedirectToAction("CategorySet");
        }

        public async Task<ActionResult> EditProduct(int productId, int categoryId)
        {
           var product= await _adminAllProducts.EditProduct(productId, categoryId);

            var viewModel = new AdminViewModel
            {
                Product = product,
            };

            return View("ProductSet",  viewModel);
        }

        public async Task<IActionResult> AddPhoto(int productId, IFormFile image, string fileExtension)
        {
            if (image != null && image.Length > 0)
            {
                using (var stream = image.OpenReadStream())
                {
                    await _adminAllProducts.AddPhoto(productId, stream, fileExtension);
                }
            }

          
            return RedirectToAction("EditProduct", new { productId = productId });
        }
       
        public async Task<ActionResult> deletePhoto (int productId, int numberImg)
        {
            await _adminAllProducts.RemovePhoto(productId, numberImg);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> movePhoto (int productId, int numberImg)
        {
            await _adminAllProducts.MovePhoto(productId, numberImg);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> editQuantity(int productId, int newQuantity)
        {
            await _adminAllProducts.EditQuantity(productId, newQuantity);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> changeAvailable(int productId, bool available)
        {
            await _adminAllProducts.ChangeAvailable(productId, available);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> changeIsFavorited(int productId, bool isFavorited)
        {
            await _adminAllProducts.ChangeIsFavorited(productId, isFavorited);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> editPropertyProduct(int productId, string propertyName, string propertyParameters)
        {
            await _adminAllProducts.EditPropertyProduct(productId, propertyName, propertyParameters);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> editHighlights(int productId, string newHighlights, string highlights)
        {
            await _adminAllProducts.EditHighlights(productId, newHighlights, highlights);
            return RedirectToAction("EditProduct", new { productId = productId });
        }

        public async Task<ActionResult> addHighlights(int productId, string addHighlights)
        {
            await _adminAllProducts.AddHighlight(productId, addHighlights);
            return RedirectToAction("EditProduct", new { productId = productId });

        }

        public async Task<ActionResult> EditProductShortDescription(int productId, string newShortDescription)
        {
            await _adminAllProducts.EditProductShortDescription(productId, newShortDescription);
            return RedirectToAction("EditProduct", new { productId = productId });

        }

        public async Task<ActionResult> EditProductLongDescription(int productId, string newLongDescription)
        {
            await _adminAllProducts.EditProductLongDescription(productId, newLongDescription);
            return RedirectToAction("EditProduct", new { productId = productId });

        }
        
        public async Task<ActionResult> editProductName(int productId, string newProductName, string productName)
        {
            await _adminAllProducts.EditProductName(productId, newProductName, productName);
            return RedirectToAction("EditProduct", new { productId = productId });

        } 
        
        public async Task<ActionResult> editProductPrice(int productId, decimal newProductPrice, decimal productPrice)
        {
            await _adminAllProducts.EditProductPrice(productId, newProductPrice, productPrice);
            return RedirectToAction("EditProduct", new { productId = productId });

        }

        public async Task<ActionResult> deleteProduct(int productId, int categoryId)
        {
            await _adminAllProducts.DeleteProductAsync(productId);

            return RedirectToAction("ProductList", new { categoryId = categoryId });
        }
    }
}
