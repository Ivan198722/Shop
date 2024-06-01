using Shop.Models;



namespace Shop.Interfaces
{
    public interface IAdminAllProducts
    {

        public Task<List<ProductInfo>> GetAllCategoriesAsync();

        public  Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsyncSortDate(int categoryId);

       // Task<IEnumerable<Product>> GetProductOfCategoryAsync(int categoryId);

        public Task AddProductAsync(int categogryId, string manufacturer, string name, decimal price);

        Task<List<ProductInfo>> GetUniqueManufacturer(int categoryId);
        
        //Task<List<Product>> GetUniqueManufacturer(int categoryId);

        Task<IEnumerable<ProductInfo>> MarkManufacturersAsync(int categoryId, string[] manufacturers);

        Task<IEnumerable<ProductInfo>> FiterProductsAsync(int categoryId, string[]? manufacturer, decimal? priceFrom, decimal? priceTo, DateTime? fromDate, DateTime? toDate);

        Task<IEnumerable<ProductInfo>> Search(int categoryId, string query);
    }
}
