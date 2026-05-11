namespace Order.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserNameAsync(string userName);
        Task AddOutboxMessageAsync(Entities.OutboxMessage outboxMessage);
    }
}
