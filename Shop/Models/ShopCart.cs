namespace Shop.Models
{
    public class ShopCart
    {
        private readonly AppDBContext _dbContext;

        public ShopCart(AppDBContext appDBContent)
        {
            this._dbContext = appDBContent;
            ListShopItems = new List<ShopCartItem>();
           

        }

        public string ShopCartId { get; set; }

        public IEnumerable<ShopCartItem> ListShopItems { get; set; }

       

    }
}
