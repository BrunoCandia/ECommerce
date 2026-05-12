namespace EventBus.Messages.Events
{
    public class PaymentCompletedEvent : BaseIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public required string UserName { get; set; }
        public required decimal TotalPrice { get; set; }
        public DateTimeOffset CompletedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
