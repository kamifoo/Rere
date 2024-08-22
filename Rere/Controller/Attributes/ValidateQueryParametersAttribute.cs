using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rere.Controller.Attributes;

/// <summary>
/// Customised attribute Only allowed parameters should be filtered in
/// </summary>
public class ValidateQueryParametersAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var queryParameters = context.HttpContext.Request.Query;

        foreach (var argument in context.ActionArguments)
        {
            var argumentValue = argument.Value;
            if (argumentValue == null)
                continue;

            var modelType = argumentValue.GetType();

            var propertyNames = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name.ToLowerInvariant())
                .ToList();

            var extraParameters = queryParameters.Keys
                .Where(q =>
                    !propertyNames.Contains(q.ToLowerInvariant()))
                .ToList();

            if (extraParameters.Count == 0) continue;

            context.Result = new BadRequestObjectResult(new
            {
                Error = $"Unknown: {string.Join(", ", extraParameters)}"
            });
        }

        base.OnActionExecuting(context);
    }
}