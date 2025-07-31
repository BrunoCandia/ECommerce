namespace Basket.Core.Entities
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; }

        public required string ProductName { get; set; }

        public required string ProductImage { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
