using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo_1.MiddleWare
{
    public class LogFilterAtribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.Exception == null)
                LogDeatils("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogDeatils("OnActionExecuting", filterContext.RouteData);
        }
        private void LogDeatils(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = string.Empty;

            if(methodName == "OnActionExecuting")
            {
                message = String.Format("{0}- controller:{1} action:{2}", "Method Start",
                                                                        controllerName,
                                                                        actionName);
            }
            else
            {
                message = String.Format("{0}- controller:{1} action:{2}", "Method End",
                                                                        controllerName,
                                                                        actionName);
            }    

            //_logger.LogInformation(message);
            Console.WriteLine(message);
        }

    }
}
