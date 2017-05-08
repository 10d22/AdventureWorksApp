using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdventureWorksApp.Controllers
{
    public class ValueReporter : ActionFilterAttribute
    {
        private void logValues(RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", "onactionexecuting", controllerName, actionName);
            Debug.WriteLine(message, "Action Values");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            logValues(filterContext.RouteData);
        }
    }
}