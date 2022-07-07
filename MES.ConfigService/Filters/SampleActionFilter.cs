using Microsoft.AspNetCore.Mvc.Filters;

namespace MES.ConfigService.Filters
{
    public class SampleActionFilter : IActionFilter
    {
        ILogger<SampleActionFilter> _logger;
        public SampleActionFilter(ILogger<SampleActionFilter> logger) => _logger = logger;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Do something before the action executes b");
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            _logger.LogInformation("Do something after the action executes e");

        }
    }
}
