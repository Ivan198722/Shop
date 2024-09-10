using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Interfaces;
using Shop.Models;
using SkiaSharp;
using System.Security.Cryptography.X509Certificates;
using static NuGet.Client.ManagedCodeConventions;

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
                images = p.Images.Where(im=>im.numberImgs==1).ToList(),
                productProperties=p.ProductProperties.ToList()

            })
            .ToListAsync();

            var randomizedProducts = products.OrderBy(_ => Guid.NewGuid()).ToList();
            return randomizedProducts;
        }

        public async Task<IEnumerable<ProductDetails>> GetProductOfCategory(int categoryId)
        {
            return await _dbContext.Products
                .Where(p => p.categoryId == categoryId)
                .Select(p=> new ProductDetails
                {
                    Product = p,
                    images = p.Images.Where(im => im.numberImgs == 1).ToList(),
                    productProperties = p.ProductProperties.ToList(),
                    highlights =  p.Highlights.ToList() 

                })
                .ToListAsync();
        }


        public async Task<IEnumerable<PropertyInfo>> GetProductProperties(int categoryId, string[] selectedModels, string[] selectedProperties)
        {
           var productIds = new List<int>();

            if (selectedModels.Length == 0)
            {
                productIds = await _dbContext.Products
                    .Where(p=>p.categoryId == categoryId)
                .Select(p => p.Id)
                .ToListAsync();
            }
            
            else if (selectedModels.Length > 0)
            {
                productIds = await _dbContext.Products
                    .Where(p=>p.categoryId == categoryId&& selectedModels.Contains(p.manufacturer))
                    .Select(p => p.Id)
                    .ToListAsync();
            }


            var propertyNames = await _dbContext.ProductProperties
                .Where(p => p.categoryId == categoryId&&productIds.Contains(p.productId))
                .Select(p => p.propertyName)
                .Distinct()
                .ToListAsync();


            var result = new List<PropertyInfo>();

            int numberProperty = 0;

            foreach (var propertyName in propertyNames)
            {
                    var propertyParameters = await _dbContext.ProductProperties
                            .Where(p => p.categoryId == categoryId && productIds.Contains(p.productId))
                            .Where(p => p.propertyName == propertyName)
                            .Select(p => p.propertyParameters).Where(p => !string.IsNullOrEmpty(p))
                            .Distinct()
                            .ToListAsync();

               

                var parameterList = propertyParameters.Select(pp => new PropertyParameter
                {
                    NumberOfParameter = ++numberProperty, 
                    Parameter = pp,
                    IsMatchingProperty = false
                }).ToList();

                result.Add(new PropertyInfo
                {
                    PropertyName = propertyName,
                    PropertyParameters= parameterList
                });

            }

            var listNumber = new List<int>();
            foreach (var item in selectedProperties)
            {
                var parts = item.Split('|');
                if (parts.Length == 2)
                {
                    var propertyName = parts[0];
                    var propertyParameter = parts[1];
                     
                    foreach(var property in result)
                    {
                      if(property.PropertyName == propertyName)
                      { 
                        foreach(var parametr in property.PropertyParameters)
                        {
                            if(parametr.Parameter == propertyParameter)
                            {
                                listNumber.Add(parametr.NumberOfParameter);
                            }
                        }
                       }
                      
                    }
                    
                }

                foreach (var property in result)
                {
                    foreach (var parameter in property.PropertyParameters)
                    {
                        parameter.IsMatchingProperty = listNumber.Contains(parameter.NumberOfParameter);
                    }
                }
            }
            
            return result;
        }

       public async Task<IEnumerable<ProductHighlights>> MarkHighlights(string[] selectedHighlights)
        {
            var highlights = await _dbContext.ProductHighlights.ToListAsync();

            foreach (var highlight in highlights)
            {
                highlight.IsMatchingHighlights = selectedHighlights.Contains(highlight.name);
            }

            return highlights;
        }

        public async Task<IEnumerable<ProductDetails>> FiterProductsAsync(int categoryId, string[] selectedModels, string[] selectedProperties,
            decimal? priceFrom, decimal? priceTo, string[] selectedHighlights)
        {
            if (selectedModels.Length == 0)
            {
                //var selectModel = _dbContext.Products.Select(p => p.manufacturer).ToArray();
                //selectedModels = selectModel;
                selectedModels = await _dbContext.Products
            .Where(p => p.categoryId == categoryId)
            .Select(p => p.manufacturer)
            .Distinct()
            .ToArrayAsync();
            }


            var idsForProperty = new List<int>();
         
            foreach (var item in selectedProperties)
            {
                var parts = item.Split('|');
                if (parts.Length == 2)
                {
                    var propertyName = parts[0];
                    var propertyParameter = parts[1];

                    if (!string.IsNullOrEmpty(propertyName)&& !string.IsNullOrEmpty(propertyParameter))
                    {
                        

                        var filtrIds = await _dbContext.ProductProperties
                            .Where(p => p.categoryId == categoryId && p.propertyName == propertyName && p.propertyParameters == propertyParameter)
                            .Select(p => p.productId)
                            .ToListAsync();

                        idsForProperty.AddRange(filtrIds);
                    }
                }
            }
            priceFrom ??= 0;
            priceTo ??= ushort.MaxValue;


            var allIds = await _dbContext.Products.
                    Where(p=>p.categoryId==categoryId)
                    .Select(p=>p.Id)
                    .ToListAsync(); 

            var idsForPrice = await _dbContext.Products
                .Where(p=>p.categoryId == categoryId  && p.price >= priceFrom
                && p.price <= priceTo)
                .Select (p => p.Id)
                .ToListAsync();

            var idsForHighlights = allIds;


            if (selectedHighlights.Length > 0)
            {

                idsForHighlights = await _dbContext.ProductHighlights
                .Where(h=>selectedHighlights.Contains(h.name))
                .Select (h=>h.productId)
                .ToListAsync();

               await MarkHighlights(selectedHighlights);
            }


            if (idsForProperty.Count == 0)
            {
                idsForProperty =allIds;
            }

            if (idsForProperty.Count > 0)
            {
               idsForProperty.Distinct().ToList();
            }
           

            var matchingIds = idsForPrice.Intersect(idsForHighlights).Intersect(idsForProperty).ToList();
            allIds = allIds.Intersect(matchingIds).ToList();

           

            

            var queryForModels = await _dbContext.Products
            .Where(p => p.categoryId == categoryId && selectedModels.Contains(p.manufacturer) )
            .Select(p => new ProductDetails
            {
                Product = p,
                images = p.Images.Where(im => im.numberImgs == 1).ToList(),
                productProperties = p.ProductProperties.ToList(),
                highlights = p.Highlights.ToList()
            })
            .ToListAsync();

           

            var filtrPropucts = queryForModels
               .Where(p => allIds.Contains(p.Product.Id) )
               
               .ToList();

            return  filtrPropucts;
           
        }

        public async Task<IEnumerable<ProductDetails>> Search(string query)
        {
            var allProduct = await GetProductDetailsAsync();

            if (string.IsNullOrWhiteSpace(query))
            {

                return allProduct;
            }


            return allProduct.Where(p=>p.Product.name.StartsWith(query, StringComparison.OrdinalIgnoreCase));

        }

    }
}
