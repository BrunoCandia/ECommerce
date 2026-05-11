using Order.Core.Common;

namespace Order.Core.Entities
{
    public class OutboxMessage : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid CorrelationId { get; set; }
        public DateTimeOffset OccuredOnUtcNow { get; set; }
        public DateTimeOffset? ProcessedOnUtcNow { get; set; }
        public bool IsProcessed => ProcessedOnUtcNow.HasValue;
        public string? Error { get; set; }
    }
}
