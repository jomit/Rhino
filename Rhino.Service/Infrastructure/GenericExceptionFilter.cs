using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Rhino.Service.Infrastructure
{
    public class GenericExceptionFilter : ExceptionFilterAttribute
    {

        public GenericExceptionFilter()
        {

        }
        public override void OnException(HttpActionExecutedContext context)
        {
            //This is the only place where try/catch should be used
            try
            {

                //log message here...

            }
            catch (Exception ex)
            {

            }
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

    }
}