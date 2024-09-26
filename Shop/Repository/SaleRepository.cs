
using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
using System.Drawing.Text;
namespace Shop.Repository
{
    public class SaleRepository : IAllSale
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
               .Include(o => o.Customer)
               .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .AsNoTracking()
               .ToListAsync();

            
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

            var orderInfo = activeOrders
                .Select(o => new OrderInfo
                {
                    Order = o,
                    Customer = o.Customer,
                    Details = ordersDetailsWithQuantity.Where(od => od.orderId == o.Id).ToList()
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
              .Include(p => p.Product)
              .ToListAsync();

            //  var soldProducts = new List<PrintOrderDetail>();
            var soldProductsDict = new Dictionary<int, PrintOrderDetail>();

            foreach (var orderDetail in orderComplete)
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


            var listByQuantityUp = soldProductsDict.Values
                .OrderByDescending(x => x.quantity)
                .ToList();

            var listByQuantityDown = soldProductsDict.Values
                .OrderBy(x => x.quantity)
                .ToList();

            var listByPriceUp = soldProductsDict.Values
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
            
        }




        public async Task<Company> GetDataCompany() => await _dbContext.Company.FirstOrDefaultAsync();


        public async Task<IEnumerable<PrintCustomer>> GetCustomers(string sort )
        {
            var orders = await _dbContext.Orders
               .Where(o => o.PaymentStatus == true)
               .Include(o => o.Customer)
               .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
              .AsNoTracking()
               .ToListAsync();

            //var customers = new List<PrintCustomer>();
            var customerDict = new Dictionary<int, PrintCustomer>();

            foreach (var order in orders)
            {
                //var existingCustomer = customers
                //    .FirstOrDefault(c => c.CustomerId == order.CustomerId);

                var totalOrderSum = order.OrderDetails.Sum(od => od.Product.price);

                //if (existingCustomer != null)
                //{
                //    existingCustomer.quantityOrders += 1;
                //    existingCustomer.totalSum += totalOrderSum;
                //}
                //else
                //{
                //    customers.Add(new PrintCustomer
                //    {
                //        CustomerId = order.CustomerId,
                //        CustomerName = order.Customer.name + " " + order.Customer.surname,
                //        quantityOrders = 1,
                //        totalSum = totalOrderSum
                //    });
                //}

                if(customerDict.TryGetValue(order.Customer.Id, out var existingCustomer ))
                {
                    existingCustomer.quantityOrders += 1;
                    existingCustomer.totalSum += totalOrderSum;
                }
                else
                {
                    customerDict[order.Customer.Id] = new PrintCustomer
                    {
                        Customer = order.Customer,
                        quantityOrders = 1,
                        totalSum = totalOrderSum
                    };
                }

            }
            var listByQuantity = customerDict.Values
                .OrderByDescending(x => x.quantityOrders)
                .ToList();

            var listBySum = customerDict.Values
                .OrderByDescending(x => x.totalSum)
                .ToList();

            

            if (string.IsNullOrEmpty(sort))
            {
                return listByQuantity;
            }
            else
            {
                switch (sort)
                {
                    case "quantity":
                        return listByQuantity;
                    
                    case "sum":
                        return listBySum;
                    
                    default: return listByQuantity;
                }
            }
        }

        public async Task<IEnumerable<OrderInfo>> GetCustomerOrders(int customerId)
        {
            var customerOrders = await _dbContext.Orders
               .Where(o => o.PaymentStatus == true && o.CustomerId==customerId)
               .Include(o => o.Customer)
               
               .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .AsNoTracking()
               .ToListAsync();


            var ordersDetailsWithQuantity = new List<PrintOrderDetail>();

            foreach (var order in customerOrders)
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

            var orderInfo = customerOrders
                .Select(o => new OrderInfo
                {
                    Order = o,
                    Customer = o.Customer,
                    Details = ordersDetailsWithQuantity.Where(od => od.orderId == o.Id).ToList()
                })
                .ToList();


            return orderInfo;

        }
    }
}
