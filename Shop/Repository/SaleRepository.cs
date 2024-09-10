
using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
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



        public async Task<IEnumerable<OrderInfo>> GetActiveOrders() =>
         await _dbContext.Orders
                .Where(o => o.PaymentStatus == true && o.CompletionStatus == false)
            .Include(o=>o.Customer)
            .Include(o=>o.OrderDetails)
          .ThenInclude(od=>od.Product)
                .Select(o => new OrderInfo
                {
                    Order = o,
                    
                    Customer = o.Customer,

                    Details = o.OrderDetails.ToList(),
                     Products= o.OrderDetails
                    .Select(od=>new Product
                    {
                        Id=od.Product.Id,
                        name=od.Product.name,
                        price=od.Product.price,
                        quantity=od.Product.quantity
                    }).ToList() 

                })
                .ToListAsync();

        public async Task<Company> GetDataCompany() => await _dbContext.Company.FirstOrDefaultAsync();

            
            

        
    }
}
