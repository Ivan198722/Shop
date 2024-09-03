using Shop.Models;



namespace Shop.Interfaces
{
    public interface IAdminAllProducts
    {

        public Task<IEnumerable<ProductInfo>> GetAllCategoriesAsync();

        public  Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsyncSortDate(int categoryId);

        public Task AddProductAsync(int categogryId, string manufacturer, string name, decimal price);

        Task<List<ProductInfo>> GetUniqueManufacturer(int categoryId);
       
        Task<IEnumerable<ProductInfo>> MarkManufacturersAsync(int categoryId, string[] manufacturers);

        Task<IEnumerable<ProductInfo>> FiterProductsAsync(int categoryId, string[]? manufacturer, decimal? priceFrom, decimal? priceTo, DateTime? fromDate, DateTime? toDate);

        Task<IEnumerable<ProductInfo>> Search(int categoryId, string query);

        Task AddProperty(int categoryId, string propertyName);

        Task EditProperty(int categoryId, string propertyName, string newPropertyName);

        Task EditDescription(int categoryId, string newDescription);

        Task AddCategory(string categoryName);

        Task EditCategory(int categoryId, string newCategoryName);

        Task<IEnumerable<ProductInfo>> EditProduct(int productId, int categoryId);

        Task AddPhoto(int Id, Stream imageStream, string fileExtension);

        Task RemovePhoto(int Id, int numberImg);

        Task MovePhoto(int Id, int numberPhoto);

        Task EditQuantity(int productId, int quantity);

        Task ChangeAvailable(int productId, bool available);

        Task ChangeIsFavorited(int productId, bool isFavorited);

        Task EditPropertyProduct(int productId, int propertyId, string propertyName, string propertyParameters);

        Task AddPropertyProduct(int categoryId, int productId, string propertyName, string propertyParameters);

        Task EditHighlights(int productId, string newHighlights, string highlights);

        Task AddHighlight(int productId, string addHighlights);

        Task EditProductShortDescription(int productId, string newShortDescription);

        Task EditProductLongDescription(int productId, string newLongDescription);

        Task EditProductName(int productId, string newProductName, string productName);

        Task EditProductPrice(int productId, decimal newProductPrice, decimal productPrice);

        Task DeleteProductAsync(int productId);



    }
}
