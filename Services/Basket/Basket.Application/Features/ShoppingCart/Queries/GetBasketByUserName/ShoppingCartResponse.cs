namespace Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; }

        public List<ShoppingCartItemResponse> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }

        public ShoppingCartResponse(string userName)
        {
            UserName = userName;
        }
    }
}
