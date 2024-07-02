


using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;


namespace Shop.Repository
{
    public class AdminRepository : IAdminAllProducts
    {
        private readonly AppDBContext _context;

        public AdminRepository(AppDBContext appContext)
        {
            _context = appContext;

        }

        public async Task<IEnumerable<ProductInfo>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                
                .Select(c => new ProductInfo 
                {
                    Category = c ,
                    productProperties = c.productProperties
                    .GroupBy(p=>p.propertyName)
                    .Select(p=>p.First())
                    .ToList()
                    
                })
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
                    .OrderBy(p => p.addDate)

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
            var newProduct = new Product
            {
                manufacturer = manufacturer,
                name = name,
                price = price,
                categoryId = categoryId,
                addDate = DateTime.Now.Date,
                isFavorited = true,
                available = true
            };

            await _context.Products.AddAsync(newProduct);

            await _context.SaveChangesAsync();


            var properties = await _context.ProductProperties
                .Where(p => p.categoryId == categoryId)
                .GroupBy(p=>p.propertyName)
                .Select(p=>p.First())
                .ToListAsync();



            foreach (var property in properties)
            {
                await _context.ProductProperties.AddAsync(new ProductProperties
                {
                    productId=newProduct.Id,
                    propertyName=property.propertyName,
                    categoryId=property.categoryId,
                });
            }
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

        public async Task AddProperty(int categoryId, string propertyName)
        {
            var products = await _context.Products
                 .Where(p => p.categoryId == categoryId)
                 .ToListAsync();

            foreach(var product in products)
            {
                await _context.ProductProperties.AddAsync(new ProductProperties
                {
                    productId = product.Id,
                    categoryId= categoryId,
                    propertyName= propertyName
                });
                
            }
             await _context.SaveChangesAsync();
            //await _context.ProductProperties.AddAsync(new ProductProperties
            //{
            //    categoryId = categoryId,
            //    propertyName = propertyName
            //});
            //await _context.SaveChangesAsync();
        }

        public async Task EditProperty(int categoryId, string propertyName, string newPropertyName)
        {
            var properties = await _context.ProductProperties
                .Where(p=>p.categoryId==categoryId&&p.propertyName==propertyName)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(newPropertyName))
            {
                foreach (var property in properties)
                {
                    property.propertyName = newPropertyName;
                }

                await _context.SaveChangesAsync();
            }

            else if (string.IsNullOrWhiteSpace(newPropertyName))
            {
                foreach(var property in properties)
                {
                    _context.ProductProperties.Remove(property);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task EditDescription(int categoryId, string newDescription)
        {
            var description = await _context.Categories
                .Where(p => p.Id == categoryId )
                .FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                description.description = newDescription;
                
                await _context.SaveChangesAsync();
            }

            else if (string.IsNullOrWhiteSpace(newDescription))
            {
               
                    _context.Categories.Remove(description);
                

                await _context.SaveChangesAsync();
            }
        }

        public async Task AddCategory(string categoryName)
        {
            await _context.Categories.AddAsync(new Category { name=categoryName } );
            await _context.SaveChangesAsync();
        }

        public async Task EditCategory(int categoryId,  string newCategoryName)
        {
            var categories = await _context.Categories
                .Where(p => p.Id == categoryId )
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(newCategoryName))
            {
                foreach (var category in categories)
                {
                    category.name = newCategoryName;
                }

                await _context.SaveChangesAsync();
            }

            else if (string.IsNullOrWhiteSpace(newCategoryName))
            {
                foreach (var category in categories)
                {
                    _context.Categories.Remove(category);
                }

                await _context.SaveChangesAsync();
            }
        }
         
        public async Task<IEnumerable<ProductInfo>> EditProduct(int productId, int categoryId)
        {
            var images = await GetProductImages(productId);
            var properties = await GetProductProperties(productId);
            var highlights = await GetHighlights(productId);
            return await _context.Products
               
                 .Where(p => p.Id == productId)
                .Select(p=> new ProductInfo
                {
                    products= new List<Product> { p},
                    images= images,
                    productProperties=properties,
                    highlights= highlights 
                })
            .ToListAsync();
        }

        public async Task<IEnumerable<Images>> GetProductImages(int productId)
        {
            return await _context.Images
                .Where(im=>im.productId==productId)
                .OrderBy(im=>im.numberImgs)
                .ToListAsync() ;
        }

        public async Task<IEnumerable<ProductProperties>> GetProductProperties(int productId)
        {
            return await _context.ProductProperties
                .Where(im=>im.productId==productId)
                .ToListAsync () ;
        }

        public async Task<IEnumerable<ProductHighlights>> GetHighlights(int productId)
        {
            return await _context.ProductHighlights
                .Where(h => h.productId == productId)
                .ToListAsync();
        }

        private byte[] GetBytesFromImage(Stream imageStream, string fileExtension)
        {
            try
            {
                using (var image = Image.Load(imageStream))
                {
                  
                    int maxWidth = 400;
                    int maxHeight = 300;

                    
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(maxWidth, maxHeight),
                        Mode = ResizeMode.Max
                    }));

                   
                    IImageEncoder encoder = fileExtension.ToLower() switch
                    {
                        ".webp" => new SixLabors.ImageSharp.Formats.Webp.WebpEncoder(),
                        _ => new PngEncoder(), // По умолчанию используем PNG
                    };

                    // Сохранение изображения в массив байтов
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, encoder);
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        private int SetNumberPhoto(int Id)
        {
            var pictures = _context.Images
                .Where(c => c.productId == Id)
                .ToList();

            if (pictures.Count == 0)
            {
                return 1;

            }

            else
            {
                var numberPhoto = pictures.Select(c => c.numberImgs).ToList();

                return numberPhoto.Max() + 1;
            }

        }

        
        public async Task AddPhoto(int Id, Stream imageStream, string fileExtension)
        {
            byte[] imageData =  GetBytesFromImage(imageStream, fileExtension);

            if (imageData != null)
            {
                _context.Images.Add(new Images
                {
                    productId = Id,
                    numberImgs = SetNumberPhoto(Id),
                    img = imageData

                });
            }

           await _context.SaveChangesAsync();
        }

        public async Task RemovePhoto(int Id, int numberImg)
        {
            var image =  _context.Images
                .Where(c => c.productId == Id && c.numberImgs == numberImg)

                .FirstOrDefault();

            if (image != null)
            {
               _context.Remove(image);


                var imageToUpdate = _context.Images
                    .Where(c => c.productId == Id && c.numberImgs > numberImg).ToList();

                foreach (var img in imageToUpdate)
                {
                    img.numberImgs -= 1;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task MovePhoto(int Id, int numberPhoto)
        {
            var listPhoto = _context.Images
                .Where(p => p.productId == Id)
                .OrderBy(c => c.numberImgs)
                .ToList();

            foreach (var img in listPhoto)
            {
                img.numberImgs++;
            }

            var moveImage = _context.Images
               .Where(p => p.productId == Id && p.numberImgs == numberPhoto).FirstOrDefault();

            if (moveImage != null)
            {
                moveImage.numberImgs = 1;
            }

            var sortImgs = _context.Images
                .Where(p => p.productId == Id && p.numberImgs != numberPhoto).OrderBy(p => p.numberImgs).ToList();

            int newNumber = 1;
            foreach (var img in sortImgs)
            {
                newNumber += 1;
                img.numberImgs = newNumber;

            }

           await _context.SaveChangesAsync();

        }

        public async Task EditQuantity (int productId, int quantity)
        {
            var product = await _context.Products
                 .Where(p => p.Id == productId)
                 .FirstOrDefaultAsync();
            if (product != null)
            {
                product.quantity = quantity;
            }
            await _context.SaveChangesAsync ();
        }

        public async Task ChangeAvailable(int productId, bool available)
        {
          var product=  await _context.Products
                .Where(p=>p.Id==productId)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.available = available;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ChangeIsFavorited(int productId, bool isFavorited)
        {
            var product = await _context.Products
                  .Where(p => p.Id == productId)
                  .FirstOrDefaultAsync();

            if (product != null)
            {
                product.isFavorited = isFavorited;
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditPropertyProduct (int productId, string propertyName, string propertyParameters)
        {
           var productProperty = await _context.ProductProperties
                .Where(p=>p.productId== productId&&p.propertyName==propertyName)
                .FirstOrDefaultAsync();

           

            if (productProperty != null)
            {
                productProperty.propertyParameters = propertyParameters;
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditHighlights(int productId, string newHighlights, string highlights)
        {
            var highlight= await _context.ProductHighlights
                .Where(h=>h.productId==productId&&h.name==highlights)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(newHighlights))
            {
                highlight.name=newHighlights;

                await _context.SaveChangesAsync();
            }

            else if(string.IsNullOrEmpty(newHighlights))
            {
                 _context.ProductHighlights.Remove(highlight);

                await _context.SaveChangesAsync();  
            }

        }

        public async Task AddHighlight(int productId, string addHighlights)

        {
            if (!string.IsNullOrEmpty(addHighlights))
            {
                await _context.ProductHighlights.AddAsync(new ProductHighlights
                {
                    productId = productId,
                    name = addHighlights,

                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task EditProductShortDescription(int productId, string newShortDescription)
        {
            var description = await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            
                description.shortDescription = newShortDescription;

                await _context.SaveChangesAsync();
            

            
        }

        public async Task EditProductLongDescription(int productId, string newLongDescription)
        {
            var description = await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            
                description.longDescription = newLongDescription;

                await _context.SaveChangesAsync();
           
        }

        public async Task EditProductName(int productId, string newProductName, string productName)
        {
            var product = await _context.Products
                .Where(p=>p.Id == productId)
                .FirstOrDefaultAsync();

            if(!string.IsNullOrEmpty(newProductName))
            {
                product.name= newProductName;

                await _context.SaveChangesAsync();
            }

            if (string.IsNullOrEmpty(newProductName)&&newProductName.Length <= 5)
            {
                product.name = productName; await _context.SaveChangesAsync();
            }
        }

        public async Task EditProductPrice(int productId, decimal newProductPrice, decimal productPrice)
        {
            var product = await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (newProductPrice!=null)
            {
                product.price = newProductPrice;

                await _context.SaveChangesAsync();
            }

            if (newProductPrice == null )
            {
                product.price = productPrice; await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                var productProperties = await _context.ProductProperties
                    .Where(p => p.productId == productId)
                    .ToListAsync();

                var images = await _context.Images
                    .Where(p => p.productId == productId)
                    .ToListAsync();

                var productHighlights = await _context.ProductHighlights
                    .Where(p => p.productId == productId)
                    .ToListAsync();

              
                _context.ProductProperties.RemoveRange(productProperties);
                _context.Images.RemoveRange(images);
                _context.ProductHighlights.RemoveRange(productHighlights);

               
                _context.Products.Remove(product);

                
                await _context.SaveChangesAsync();
            }
        }
    }

}
