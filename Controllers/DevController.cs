#if DEBUG
// ! ONLY RUN THIS IN DEBUG !
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ModelSaber.Main.Controllers
{
    [ApiController]
    public class DevController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public DevController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet("api/routes")]
        public IActionResult GetAllRoutes()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(t => t.AttributeRouteInfo != null)
                .Select(t => new
                {
                    Action = t is { RouteValues: { } } && !string.IsNullOrWhiteSpace(t.RouteValues["action"]) ? t.RouteValues["action"] : "n/a",
                    Controller = t is { RouteValues: { }} && !string.IsNullOrWhiteSpace(t.RouteValues["controller"]) ? t.RouteValues["controller"] : "n/a",
                    Name = t.AttributeRouteInfo?.Name ?? "n/a",
                    Template = t.AttributeRouteInfo?.Template ?? "n/a",
                    Methods = t.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.ToList()
                }).ToList();
            return Ok(routes);
        }
    }
}
#endif