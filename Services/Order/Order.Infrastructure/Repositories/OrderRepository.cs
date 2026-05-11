using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Core.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Core.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<Core.Entities.Order>> GetOrdersByUserNameAsync(string userName)
        {
            var orderList = await _orderContext.Orders
                    .Where(o => o.UserName == userName)
                    .ToListAsync();

            return orderList;
        }

        public async Task AddOutboxMessageAsync(OutboxMessage outboxMessage)
        {
            await _orderContext.OutboxMessages.AddAsync(outboxMessage);
            await _orderContext.SaveChangesAsync();
        }
    }
}
