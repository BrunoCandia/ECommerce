namespace EventBus.Messages.Events
{
    public class PaymentFailedEvent : BaseIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public required string UserName { get; set; }
        public string? FailureReason { get; set; }
        public DateTimeOffset FailedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
