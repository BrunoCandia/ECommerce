namespace Order.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserNameAsync(string userName);
    }
}
