using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Repository
{
    public class ProductRepository : IAllProducts
    {
        private readonly AppDBContext _dbContext;

        public ProductRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<IEnumerable<ProductDetails>> GetProductDetailsAsync()
        {
           var products = await _dbContext.Products
                .Where(p=>p.isFavorited==true)
            .Select(p => new ProductDetails
            {
                Product = p,
                images = p.Images.Where(im=>im.numberImgs==1).ToList()

            })
            .ToListAsync();

            var randomizedProducts = products.OrderBy(_ => Guid.NewGuid()).ToList();
            return randomizedProducts;
        }
    }
}
