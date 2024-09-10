using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class OrderController : BaseController
    {
        

        private readonly IAllOrders _allOrders;

       private readonly IAllShopCart _allShopCart;

        private readonly ShopCart _shopCart;

        public OrderController(IAdminAllProducts adminRepository, IAllShopCart allShopCart, IAllOrders allOrders, ShopCart shopCart) : base(adminRepository, allShopCart)
        {
            
            _allOrders = allOrders;
            _allShopCart = allShopCart;
            _shopCart = shopCart;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
       
         
        public async Task< IActionResult> Checkout(string email)
        {
            var customer = await _allOrders.GetCustomer(email);

            string mail = email;

            var viewModel = new OrderViewModel
            {
                Customer = customer,
                Email = mail

            };

            
            return View(viewModel);
        }

        public async Task<IActionResult> CheckEmail(string email)
        {
                return RedirectToAction("Checkout", new { email = email });
            
        }

        public async Task<IActionResult> Order(int customerId, string email, string name, string surname, string city, string postcode,
             string street, string numberHouse, string numberFlat, string phone, string NIP)
        {
            var customer = new Customer
            {
                Id = customerId,
                email = email,
                name = name,
                surname = surname,
                city = city,
                postcode = postcode,
                street = street,
                NumberHouse = numberHouse,
                NumberFlat = numberFlat,
                phone = phone,
                NIP = NIP
            };

            var items = await _allShopCart.GetShopCartItemsAsync();
            _shopCart.ListShopItems = items;

            var totalPrice = await _allShopCart.TotalPrice();
            
            var viewModel = new OrderViewModel
            {
                OrderCustomer = customer,
                ShopCart= _shopCart,
                totalPrice = totalPrice,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int customerId, string email, string name, string surname, string city, string postcode,
            string street, string numberHouse, string numberFlat, string phone, string NIP)
        {
            await _allOrders.CompleteOrder(customerId, email, name, surname, city, postcode, street, numberHouse, numberFlat, phone, NIP);

            if(_allOrders.HasInsufficientItems)
            {
                TempData["InsufficientItems"] = "Brak towru";
                return RedirectToAction("IndexShopCart", "ShopCart"); 
            }
            else
            {
                return RedirectToAction("Complete");
            }
        }

        


        public IActionResult Complete()
        {
            ViewBag.Message = "Zamówienie zostało zrealizowsne!";
            return View();
        }
    }
}
