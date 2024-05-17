


using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;



namespace Shop.Repository
{
    public class AdminRepository : IAdminAllProducts
    {
        private readonly AppDBContext _context;

        public AdminRepository(AppDBContext appContext)
        {
            _context = appContext;

        }

        //public IEnumerable<ProductInfo> GetAllCategories => _context.Categories
        //  .Select(c => new ProductInfo { Category = c }).ToList();

        public async Task<List<ProductInfo>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new ProductInfo { Category = c })
                .ToListAsync();
        }


        //public IEnumerable<ProductInfo> GetProductOfCategory(int categoryId)
        //{

        //    return _context.Categories
        //        .Where(c => c.Id == categoryId)
        //        .Select(c => new ProductInfo
        //        {
        //            Category = c,
        //            products=c.products.Where(c => c.Id == categoryId).ToList()

        //        })
        //        .ToList();
        //}
        public async Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsync(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new ProductInfo
                {
                    Category = c,
                    products = c.products.Where(p => p.categoryId == categoryId).ToList()
                })
                .ToListAsync();
        }

       

    }

}
