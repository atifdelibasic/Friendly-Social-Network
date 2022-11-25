using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Friendly.WebAPI.ActionFilters
{
    public class DataValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
         
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
