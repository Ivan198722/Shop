using Microsoft.EntityFrameworkCore;
using Shop.Interfaces;
using Shop.Models;
using System;

namespace Shop.Repository
{
    public class OrderRepository:IAllOrders

    {
        public readonly AppDBContext _dbContext;
        public readonly IAllShopCart _shopCart;

        public OrderRepository(AppDBContext appDBContext, IAllShopCart shopCart)
        {
            _dbContext = appDBContext;
            _shopCart = shopCart;
        }

        public async Task<Customer> GetCustomer(string emailAddress)
        {
            var dataCustomer = await _dbContext.Customers
                .Where(c=>c.email == emailAddress)
                .FirstOrDefaultAsync();

            if (dataCustomer != null)
            {
                return dataCustomer;
            }

            return null;
        }

        public async Task<Customer> AddCustomer(string email, string name, string surname, string city, string postcode,
             string street, string numberHouse, string numberFlat, string phone, string NIP)
        {

            var customer = new Customer
            {
                email = email,
                name = name,
                surname = surname,
                city = city,
                postcode = postcode,
                street = street,
                NumberHouse = numberHouse,
                NumberFlat = numberFlat,
                phone = phone,
                NIP=NIP
            };

           await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();    

            return customer;
        }

      
        public async Task<int> UpdateCustomer(Customer customer, string email, string name, string surname, string city, string postcode,
         string street, string numberHouse, string numberFlat, string phone,string NIP)
        {
            if (customer == null) return -1;  

            bool isModified = false;

            
            if (customer.email != email)
            {
                customer.email = email;
                isModified = true;
            }
            if (customer.name != name)
            {
                customer.name = name;
                isModified = true;
            }
            if (customer.surname != surname)
            {
                customer.surname = surname;
                isModified = true;
            }
            if (customer.city != city)
            {
                customer.city = city;
                isModified = true;
            }
            if (customer.postcode != postcode)
            {
                customer.postcode = postcode;
                isModified = true;
            }
            if (customer.street != street)
            {
                customer.street = street;
                isModified = true;
            }
            if (customer.NumberHouse != numberHouse)
            {
                customer.NumberHouse = numberHouse;
                isModified = true;
            }
            if (customer.NumberFlat != numberFlat)
            {
                customer.NumberFlat = numberFlat;
                isModified = true;
            }
            if (customer.phone != phone)
            {
                customer.phone = phone;
                isModified = true;
            }
            if (customer.NIP != NIP)
            {
                customer.NIP = NIP;
                isModified = true;
            }
            if (isModified)
            {
                await _dbContext.SaveChangesAsync();
               
            }

            return customer.Id;
        }

        public async Task<int> CheckCustomer(int customerId, string email, string name, string surname, string city, string postcode,
            string street, string numberHouse, string numberFlat, string phone, string NIP)
        {
            var customer = await _dbContext.Customers
                .Where(c => c.Id == customerId)
                .FirstOrDefaultAsync();

            if (customer != null)
            {
               return await UpdateCustomer(customer, email, name, surname, city, postcode, street, numberHouse, numberFlat, phone, NIP);

               
            }
            else
            {
                var addCustomer =  await  AddCustomer(email, name, surname, city, postcode, street, numberHouse, numberFlat, phone, NIP);

             return addCustomer.Id;
                
            }
        }

       

        public async Task<Order> CreateOrder(int customerId)
        {
          var order = (new Order
            {
                CustomerId = customerId,
                OrderTime = DateTime.Now,
                PaymentStatus=false,
                CompletionStatus=false,
            });

            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
            
        }


        public async Task<int> QuantityProduct(int productId)
        {
            return await _dbContext.Products.Where(c => c.Id == productId).Select(q => q.quantity).FirstOrDefaultAsync();
        }
       


         
        
        
        public async Task CompleteOrder(int customerId, string email, string name, string surname, string city, string postcode,
    string street, string numberHouse, string numberFlat, string phone, string NIP)
       
        {
            try
            {
                var idCustomer = await CheckCustomer(customerId, email, name, surname, city, postcode, street, numberHouse, numberFlat, phone, NIP);

                var order = await CreateOrder(idCustomer);

                var items = await _shopCart.GetShopCartItemsAsync();
                bool hasInsufficientItems = false;

                foreach (var item in items)
                {
                    var quantityAvailable = await QuantityProduct(item.product.Id); 

                    if (quantityAvailable >= item.quantity)
                    {
                        hasInsufficientItems = false;
                        
                        var orderDetail = new OrderDetail()
                        {
                            orderId = order.Id,
                            productID = item.product.Id,
                             quantity = item.quantity,
                            price = item.price
                        };

                        _dbContext.OrderDetails.Add(orderDetail);
                       

                        
                        var productToUpdate = await _dbContext.Products.FindAsync(item.product.Id);
                        productToUpdate.quantity -= item.quantity;
                    }
                    else
                    {
                        hasInsufficientItems = true;
                        break;

                    }
                }



                if (hasInsufficientItems)
                {
                    HasInsufficientItems = true;
                }
                else
                {
                    HasInsufficientItems = false; 
                }

                if (!HasInsufficientItems)
                {
                    _dbContext.ShopCartItems.RemoveRange(items);

                    order.PaymentStatus=true;
                   
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

               

                 
        }

            public bool HasInsufficientItems { get; private set; } = false;

    }
}

