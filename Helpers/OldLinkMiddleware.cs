using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ModelSaber.Database;

namespace ModelSaber.Main.Helpers
{
    public class OldLinkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _provider;

        public OldLinkMiddleware(RequestDelegate next, IServiceProvider provider)
        {
            _next = next;
            _provider = provider;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("/api") || !context.Request.Query.ContainsKey("id"))
            {
                await _next(context);
                return;
            }

            var id = context.Request.Query["id"].ToString();
            if (string.IsNullOrWhiteSpace(id))
                context.Response.Redirect("/", true);
            else
            {
                var key = Convert.ToUInt32(id);
                //TODO make this get from static list loaded on first execute from global variable in Program
                using var scope = _provider.CreateScope();
                await using var dbContext = scope.ServiceProvider.GetService<ModelSaberDbContext>();
                if(dbContext == null)
                    context.Response.Redirect("/", true);
                else
                {
                    var model = dbContext!.Models.SingleOrDefault(t => t.Id == key);
                    if (model == null)
                        context.Response.Redirect("/", true);
                    else
                        context.Response.Redirect("/model/" + model!.Uuid, true);
                }
            }
        }
    }
}
