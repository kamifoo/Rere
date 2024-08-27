using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rere.Controllers.Attributes;

public class GuardModelStateAttribute(
    string detail = "The request contains invalid data.")
    : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Detail = detail,
                Status = (int)HttpStatusCode.BadRequest,
                Instance = context.HttpContext.Request.Path
            };

            foreach (var key in context.ModelState.Keys)
            {
                var errors = context.ModelState[key]?.Errors;
                if (errors is not { Count: > 0 }) continue;
                foreach (var error in errors) problemDetails.Extensions.Add(key, error.ErrorMessage);
            }

            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}