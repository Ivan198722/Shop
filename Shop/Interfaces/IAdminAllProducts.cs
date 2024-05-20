using Shop.Models;



namespace Shop.Interfaces
{
    public interface IAdminAllProducts
    {

        public Task<List<ProductInfo>> GetAllCategoriesAsync();

       public  Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsync(int categoryId);

        public Task AddProductAsync(int categogryId, string manufacturer, string name, ushort price);

    }
}
