using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharingApplication.Controllers
{
    public class ValueReporter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            logValues(filterContext.RouteData);
        }

        private void logValues(RouteData routeData)
        {
            foreach (var value in routeData.Values)
            {
                Debug.WriteLine( value.Key + ": " + value.Value );
            }
        }
    }
}