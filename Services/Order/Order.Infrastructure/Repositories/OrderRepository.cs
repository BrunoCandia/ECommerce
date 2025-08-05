using Microsoft.EntityFrameworkCore;
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
    }
}
