namespace Order.Core.Common
{
    public abstract class BaseEntity
    {
        //Protected set is made to use in the derived classes
        public Guid Id { get; protected set; }

        //Below Properties are Audit properties
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateNow { get; set; }
        public DateTime? CreatedDateUtcNow { get; set; }
        public DateTimeOffset? CreatedDateTimeOffsetNow { get; set; }
        public DateTimeOffset? CreatedDateTimeOffsetUtcNow { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
