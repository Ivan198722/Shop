
using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
using System.Drawing.Text;
namespace Shop.Repository
{
    public class SaleRepository:IAllSale
    {
      private readonly AppDBContext _dbContext;

        public SaleRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<int> GetCountActiveOrders() =>
            await _dbContext.Orders
                .Where(o => o.PaymentStatus == true && o.CompletionStatus == false)
                .CountAsync();



        public async Task<IEnumerable<OrderInfo>> GetActiveOrders()
        {
            var activeOrders = await _dbContext.Orders
               .Where(o => o.PaymentStatus == true && o.CompletionStatus == false)
               .Include(o=>o.Customer)
               .Include(o=>o.OrderDetails)
               .ThenInclude(od=>od.Product)
               .ToListAsync();

            var itemOrders = await _dbContext.Orders
             .Where(o => o.PaymentStatus == true && o.CompletionStatus == false)
             .Include(o => o.Customer)
             
             .Select(o => new OrderInfo
             {
                 Order = o,
                 Customer = o.Customer
             })
             .ToListAsync();

            // var ordersDetailsWithQuantity = await GetOrderDetailsWithQuantity(activeOrders);
            var ordersDetailsWithQuantity = new List<PrintOrderDetail>();

            foreach (var order in activeOrders)
            {

                foreach (var detail in order.OrderDetails)
                {

                    var existingDetail = ordersDetailsWithQuantity
                        .FirstOrDefault(od => od.productID == detail.productID && od.orderId == detail.orderId);

                    if (existingDetail != null)
                    {
                        existingDetail.quantity += 1;
                        existingDetail.price = detail.Product.price * existingDetail.quantity;
                    }
                    else
                    {
                        var orderDetail = new PrintOrderDetail
                        {
                            Id = detail.Id,
                            productID = detail.productID,
                            orderId = detail.orderId,
                            quantity = 1,
                            price = detail.Product.price,
                            Product = detail.Product
                        };

                        ordersDetailsWithQuantity.Add(orderDetail);
                    }
                }
            }

            var orderInfo = itemOrders
                .Select(o => new OrderInfo
                {
                    Order = o.Order,
                    Customer = o.Customer,
                    Details = ordersDetailsWithQuantity.Where(od => od.orderId == o.Order.Id).ToList()
                })
                .ToList();

          
            return orderInfo;
         
        }

       

        public async Task FinishOrder(int orderId)
        {
            var order = await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            var finishOrder = new FinishedOrder
            {
                orderId = orderId,
                orderTime = DateTime.Now
            };

            await _dbContext.FinishedOrders.AddAsync(finishOrder);
            order.CompletionStatus = true;
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<PrintOrderDetail>> GetProductsSold(string sort)
        {
              var orderComplete = await (
                from od in _dbContext.OrderDetails
                join o in _dbContext.Orders on od.orderId equals o.Id
                 where o.PaymentStatus == true && o.CompletionStatus == true
                 
                select od
                )
                .Include(p=>p.Product)
                .ToListAsync();

          //  var soldProducts = new List<PrintOrderDetail>();
            var soldProductsDict = new Dictionary<int, PrintOrderDetail>();

            foreach ( var orderDetail in orderComplete )
            {
                //var existingDetail = soldProducts
                //       .FirstOrDefault(od => od.productID == orderDetail.productID );
                //// var key = (orderDetail.productID);
                //if (existingDetail != null)
                //{
                //    existingDetail.quantity += 1;
                //    existingDetail.price = orderDetail.Product.price * existingDetail.quantity;
                //}
                if (soldProductsDict.TryGetValue(orderDetail.productID, out var existingDetail))
                {
                    // Продукт уже существует, обновляем количество и цену
                    existingDetail.quantity += 1;
                    existingDetail.price = orderDetail.Product.price * existingDetail.quantity;
                }
                //else
                //{
                //    soldProducts.Add(new PrintOrderDetail
                //    {
                //        Id = orderDetail.Id,
                //        productID = orderDetail.productID,
                        
                //        quantity = 1,
                //        price = orderDetail.Product.price,
                //        Product = orderDetail.Product
                //    }
                //    );

                //}
                 else
                {
                    // Продукт не существует, добавляем новый
                    soldProductsDict[orderDetail.productID] = new PrintOrderDetail
                    {
                        Id = orderDetail.Id,
                        productID = orderDetail.productID,
                        quantity = 1, 
                        price = orderDetail.Product.price, 
                        Product = orderDetail.Product 
                    };
                }
            }


            var listByQuantityUp= soldProductsDict.Values
                .OrderByDescending(x => x.quantity)
                .ToList();

            var listByQuantityDown= soldProductsDict.Values
                .OrderBy(x => x.quantity)
                .ToList();

            var listByPriceUp= soldProductsDict.Values
                .OrderByDescending(x => x.price)
                .ToList();

            var listByPriseDown = soldProductsDict.Values
                .OrderBy(x => x.price)
                .ToList();

            if (string.IsNullOrEmpty(sort))
            {
                return listByQuantityUp;
            }
            else
            {
                switch (sort)
                {
                    case "quantityUp":
                        return listByQuantityUp;
                    case "quantityDown":
                        return listByQuantityDown;
                    case "priceUp":
                        return listByPriceUp;
                    case "priceDown":
                        return listByPriseDown;
                    default: return listByQuantityUp;
                }
            }
            //  return soldProducts;
          // return soldProductsDict.Values;
        }


           

        public async Task<Company> GetDataCompany() => await _dbContext.Company.FirstOrDefaultAsync();

       

       


    }
}
