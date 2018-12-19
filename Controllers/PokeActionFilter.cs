using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CSharp_Pline.Controllers
{
  public class PokeActionFilter : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      ILogger<PokeActionFilter> log = (ILogger<PokeActionFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<PokeActionFilter>));
      log.LogInformation(context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
      System.Console.WriteLine(context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      ILogger<PokeActionFilter> log = (ILogger<PokeActionFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<PokeActionFilter>));
      log.LogInformation(context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
      System.Console.WriteLine(context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
      
    }

    
  }
}