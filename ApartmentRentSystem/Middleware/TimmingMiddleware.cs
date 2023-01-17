namespace ApartmentRentSystem.Middleware
{
    public class TimmingMiddleware
    {
        private readonly ILogger<TimmingMiddleware> logger;
        private readonly RequestDelegate requestDelegate;
        public TimmingMiddleware(ILogger<TimmingMiddleware> logger, RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var start = DateTime.UtcNow;
            await requestDelegate.Invoke(httpContext);

            logger.LogInformation($"Request {httpContext.Request.Path}: {DateTime.UtcNow - start}.");
        }
    }
}
