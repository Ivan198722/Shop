using Shop.Models;



namespace Shop.Interfaces
{
    public interface IAdminAllProducts
    {

        public Task<List<ProductInfo>> GetAllCategoriesAsync();

       public  Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsync(int categoryId);

    }
}
