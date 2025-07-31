namespace Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart
{
    public class ShoppingCartItemRequest
    {
        public string ProductId { get; set; }

        public required string ProductName { get; set; }

        public required string ProductImage { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
