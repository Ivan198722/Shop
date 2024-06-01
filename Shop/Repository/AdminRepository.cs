


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

        public async Task<List<ProductInfo>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                
                .Select(c => new ProductInfo { Category = c })
                .ToListAsync();
        }

        public async Task<List<ProductInfo>> GetUniqueManufacturer(int categoryId)
        {
            return await _context.Products
             
                .Where(p=>p.categoryId == categoryId)
                .GroupBy(g=>g.manufacturer)
                .Select(p => new ProductInfo
                {
                    products = new List<Product> {p.First()}
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsyncSortDate(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new ProductInfo
                {
                    Category = c,
                    products = c.products
                    .Where(p => p.categoryId == categoryId)
                    .OrderByDescending(p => p.addDate)

                    .ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInfo>> GetProductOfCategoryAsyncSortPrice(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new ProductInfo
                {
                    Category = c,
                    products = c.products
                    .Where(p => p.categoryId == categoryId)
                    .OrderByDescending(p => p.price)

                    .ToList()
                })
                .ToListAsync();
        }

        public async Task AddProductAsync(int categoryId, string manufacturer, string name, decimal price)
        {
            await _context.Products.AddAsync(new Product
            {
                manufacturer = manufacturer,
                name = name,
                price = price,
                categoryId = categoryId,
                addDate = DateTime.Now.Date
                
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductInfo>> MarkManufacturersAsync(int categoryId, string[] manufacturers)
        {
            var produсtMarkedManufacturers = await _context.Categories
                .Where(c => c.Id == categoryId)
                
                .Select(c => new ProductInfo
                {
                    
                    products = c.products
                        .Where(p => p.categoryId == categoryId)
                        .GroupBy (g => g.manufacturer)
                        .Select(g=>g.First())
                        .ToList()
                })
                .ToListAsync();

            foreach (var productInfo in produсtMarkedManufacturers)
            {
                foreach (var product in productInfo.products)
                {
                    product.IsMatchingManufacturer = manufacturers.Contains(product.manufacturer);
                }
            }

            return produсtMarkedManufacturers;
        }

        public async Task<IEnumerable<ProductInfo>> FiterProductsAsync(int categoryId, string[]? manufacturers, decimal? priceFrom, decimal? priceTo, DateTime? fromDate, DateTime? toDate)
        {
            if (manufacturers.Length == 0)
            {
                var selectManufacturer = _context.Products.Select(p => p.manufacturer).ToArray();
                manufacturers = selectManufacturer;
             
            }
            if (priceFrom == null)
            {
                priceFrom = 0;
            }
            if (priceTo == null)
            {
                priceTo = ushort.MaxValue;
            }
            if (fromDate == null)
            {
                fromDate = DateTime.MinValue;
            }
            if (toDate == null)
            {
                toDate = DateTime.MaxValue;
            }

            return await _context.Categories
                .Where(c=>c.Id==categoryId)

                .Select(p=> new ProductInfo
                {
                    Category=p,
                    products=p.products
                    .Where(p=>manufacturers.Contains(p.manufacturer) 
                && p.price>= priceFrom 
                && p.price<= priceTo
                && p.addDate>= fromDate
                && p.addDate<=toDate)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInfo>> Search(int categoryId, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
               
                return await GetProductOfCategoryAsyncSortPrice(categoryId);
            }

          
            return await _context.Categories
                .Where (c=>c.Id==categoryId)
                .Select(c => new ProductInfo
                {
                    Category= c, 
                    
                    products = c.products.Where(p=>p.name.StartsWith(query)).ToList()
                })
                .ToListAsync();
        }
    }

}
