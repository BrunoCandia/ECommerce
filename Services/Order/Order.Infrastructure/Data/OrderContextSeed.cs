using Microsoft.Extensions.Logging;

namespace Order.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());

                await orderContext.SaveChangesAsync();

                logger.LogInformation("Order Database: {Name} seeded!!!", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Core.Entities.Order> GetOrders()
        {
            return new List<Core.Entities.Order>
            {
                new()
                {
                    UserName = "jhon",
                    FirstName = "Jhon",
                    LastName = "Doe",
                    EmailAddress = "admin@eCommerce.net",
                    AddressLine = "any address",
                    Country = "USA",
                    TotalPrice = 750,
                    State = "IN",
                    ZipCode = "560001",

                    CardName = "Visa",
                    CardNumber = "1234567890123456",
                    CreatedBy = "Jhon",
                    Expiration = "12/26",
                    Cvv = "123",
                    PaymentMethod = 1
                }
            };
        }
    }
}
