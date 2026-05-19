using Serilog.Context;

namespace Ocelot.ApiGateway.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "x-correlation-id";
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Generate a new correlation ID if it doesn't exist in the incoming request headers
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[CorrelationIdHeader] = correlationId;
            }

            context.Request.Headers[CorrelationIdHeader] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                _logger.LogInformation("Processing request with Correlation ID: {CorrelationId}", correlationId);
                await _next(context);
            }
        }
    }
}
