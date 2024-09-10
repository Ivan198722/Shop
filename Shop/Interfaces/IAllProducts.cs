using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllProducts
    {

        Task<IEnumerable<ProductDetails>> GetProductDetailsAsync();
        Task<IEnumerable<ProductDetails>> GetProductOfCategory(int categoryId);
        Task<IEnumerable<ProductDetails>> Search(string query);

        Task<IEnumerable<PropertyInfo>> GetProductProperties(int categoryId, string[] selectedModels, string[] selectedProperties);


        Task<IEnumerable<ProductDetails>> FiterProductsAsync(int categoryId, string[] selectedModels, string[] selectedProperties,
            decimal? priceFrom, decimal? priceTo, string[] selectedHighlights);

    }
}
