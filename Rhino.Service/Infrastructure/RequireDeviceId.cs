using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Rhino.Service.Infrastructure
{
    public class RequireDeviceId : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // Get the sender address
            //var currentRequest = ((HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
            //var serverName = currentRequest.ServerVariables["SERVER_NAME"];
            //if (serverName.Contains("indigoslate.azurewebsites.net") || serverName.Contains("localhost"))
            //{
            //    return;
            //}
            var header = actionContext.Request.Headers.SingleOrDefault(x => x.Key == "DeviceId");
            var valid = header.Value != null || DeviceManager.IsValid(header.Value.First());
            if (!valid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid Device Id");
            }
        }
    }
}