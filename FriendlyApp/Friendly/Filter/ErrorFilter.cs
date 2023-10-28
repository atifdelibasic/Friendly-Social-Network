using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Friendly.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SendGrid.Helpers.Errors.Model;

namespace Friendly.WebAPI.Filter
{
    public class ErrorFilter : ExceptionFilterAttribute
    {
        private const string INTERNAL_SERVER_ERROR_MSG = "An internal server error occurred. Please contact the administrator.";
        private const int INTERNAL_SERVER_ERROR_CODE = (int)HttpStatusCode.InternalServerError;
        private const int BAD_REQUEST_CODE = (int)HttpStatusCode.BadRequest;
        private const int NOT_FOUND_CODE = (int)HttpStatusCode.NotFound;
        private const int UNAUTHORIZED_CODE = (int)HttpStatusCode.Unauthorized;

        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case UserException userException:
                    context.ModelState.AddModelError("error", userException.Message);
                    context.HttpContext.Response.StatusCode = BAD_REQUEST_CODE;
                    break;
                case NotFoundException notFoundException:
                    context.ModelState.AddModelError("error", notFoundException.Message);
                    context.HttpContext.Response.StatusCode = NOT_FOUND_CODE;
                    break;
                case UnauthorizedAccessException unauthorizedException:
                    context.ModelState.AddModelError("error", unauthorizedException.Message);
                    context.HttpContext.Response.StatusCode = UNAUTHORIZED_CODE;
                    break;
                default:
                    context.ModelState.AddModelError("error", INTERNAL_SERVER_ERROR_MSG);
                    context.HttpContext.Response.StatusCode = INTERNAL_SERVER_ERROR_CODE;
                    // Log the exception 
                    // ex.LogError();
                    break;
            }
            context.Result = new JsonResult(new ErrorResponse(context.ModelState));
        }
    }
    public class ErrorResponse
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; }
        public ErrorResponse(ModelStateDictionary modelState)
        {
            Errors = modelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, y => y.Value.Errors.Select(z => z.ErrorMessage));
        }
    }
}
