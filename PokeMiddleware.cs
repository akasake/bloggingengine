using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CSharp_Pline
{
  public class PokeMiddleware
  {
    private readonly RequestDelegate _next;

    public PokeMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      Console.WriteLine("Hello from the middleware");
      // Call the next delegate/middleware in the pipeline
      await _next(context);
    }
  }

  public static class PokeMiddlewareExtensions
  {
    public static IApplicationBuilder UsePokeMiddleware(
            this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<PokeMiddleware>();
    }
  }
}