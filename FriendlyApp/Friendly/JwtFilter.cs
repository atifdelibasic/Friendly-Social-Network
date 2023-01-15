using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class JwtFilterAttribute : ActionFilterAttribute
{
   
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            var principal = JwtHelper.GetPrincipalFromToken(token);
            if (principal != null)
            {
                var userId = principal.FindFirst("userid").Value;
                context.HttpContext.Items["UserId"] = userId;
            }
        }
        base.OnActionExecuting(context);
    }
}