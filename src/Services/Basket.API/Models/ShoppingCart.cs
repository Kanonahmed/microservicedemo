namespace Basket.API.Models
{
    public class ShoppingCart
    {
        ShoppingCart(string username)
        {
            UserName= username;
        }
        ShoppingCart() { }

        public string UserName { get; set; }
        public List<ShoppingCartItem> items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice { 
            get
            {
                decimal totalPrice = 0;
                foreach (var item in items)
                {
                    totalPrice += item.Price;
                }
                return totalPrice; 
            }
                }
    }
}
