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

        Task<IEnumerable<ProductInfo>> EditProduct(int productId);

        Task AddPhoto(int Id, Stream imageStream, string fileExtension);

        Task RemovePhoto(int Id, int numberImg);

        Task MovePhoto(int Id, int numberPhoto);
    }
}
