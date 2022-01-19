using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModelSaber.Database;

namespace ModelSaber.Main.Helpers
{
    public class OldLinkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ModelSaberDbContext _dbContext;

        public OldLinkMiddleware(RequestDelegate next, ModelSaberDbContext dbContext)
        {
            _next = next;
            _dbContext = dbContext;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("/api") || !context.Request.Query.ContainsKey("id"))
                await _next(context);

            var id = context.Request.Query["id"].ToString();
            if (string.IsNullOrWhiteSpace(id))
                context.Response.Redirect("/");
            else
            {
                var key = Convert.ToUInt32(id);
                //TODO make this get from static list loaded on first execute from global variable in Program
                var model = _dbContext.Models.SingleOrDefault(t => t.Id == key);
                if (model == null)
                    context.Response.Redirect("/");
                else
                    context.Response.Redirect("/model/" + model!.Uuid);
            }

            await _next(context);
        }
    }
}
