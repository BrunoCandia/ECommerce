using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Data;
using System.Text.Json;

namespace Order.Infrastructure.Dispatcher
{
    public class OutboxMessageDispatcher : BackgroundService
    {
        private readonly ILogger<OutboxMessageDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        public OutboxMessageDispatcher(ILogger<OutboxMessageDispatcher> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Wait 1 minute before the first run
            _logger.LogInformation("Delaying first outbox dispatch run for {Delay}", TimeSpan.FromMinutes(1));
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Shutdown requested before first run
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var pendingMessages = await dbContext.OutboxMessages
                    .Where(o => o.ProcessedOnUtcNow == null)
                    .OrderBy(o => o.OccuredOnUtcNow)
                    .Take(100)
                    .ToListAsync();

                foreach (var message in pendingMessages)
                {
                    try
                    {
                        var orderCheckedoutEvent = JsonSerializer.Deserialize<OrderCheckoutEvent>(message.Content);

                        await publishEndpoint.Publish(orderCheckedoutEvent!, stoppingToken);

                        message.ProcessedOnUtcNow = DateTimeOffset.UtcNow;

                        _logger.LogInformation("Published outbox message with id {MessageId} and type {MessageType}", message.Id, message.Type);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, "Error publishing outbox message with id {MessageId} and type {MessageType}", message.Id, message.Type);
                        throw;
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
