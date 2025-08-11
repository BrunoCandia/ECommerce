namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {
        public Guid CorrelationId { get; set; }

        public DateTimeOffset CreationDate { get; private set; }

        public BaseIntegrationEvent()
        {
            CorrelationId = Guid.NewGuid();
            CreationDate = DateTimeOffset.UtcNow;
        }

        public BaseIntegrationEvent(Guid correlationId, DateTimeOffset creationDate)
        {
            CorrelationId = correlationId;
            CreationDate = creationDate;
        }
    }
}
