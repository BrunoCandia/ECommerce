using Basket.Core.Entities;

namespace Basket.Core.Repositories
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Gets the shopping cart for a specific user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The shopping cart for the user.</returns>
        Task<ShoppingCart?> GetBasketAsync(string userName);

        /// <summary>
        /// Saves the shopping cart for a specific user.
        /// </summary>
        /// <param name="shoppingCart">The shopping cart to save.</param>
        /// <returns>The saved shopping cart.</returns>
        Task<ShoppingCart?> UpsertBasketAsync(ShoppingCart shoppingCart);

        /// <summary>
        /// Deletes the shopping cart for a specific user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        Task DeleteBasketAsync(string userName);

        /// <summary>
        /// Checks out the basket and returns a checkout object.
        /// </summary>
        Task<BasketCheckout> CheckoutBasketAsync(BasketCheckout basketCheckout);
    }
}
