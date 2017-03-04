using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Project.WebUI.Infrastructure.Core;

namespace Project.WebUI.Filters
{
    public class DiagnosticAttribute : ActionFilterAttribute
    {
        Stopwatch swatch = new Stopwatch();
        string ActionName;
        string ControllerName;
        string UserName;
        string Url;
        string ipAddress;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {          
            ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            if (filterContext.HttpContext.User != null)
                UserName = filterContext.HttpContext.User.Identity.Name;
            else
                UserName = null;
            ActionName = filterContext.ActionDescriptor.ActionName;
            ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            Url = filterContext.HttpContext.Request.Url.AbsoluteUri;
            swatch.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            swatch.Stop();
            Log.Logger.Trace("Controller:{0};Action:{1};UserName:{2};Url:{3};time:{4}ms", ControllerName, ActionName, UserName, Url, swatch.ElapsedMilliseconds.ToString());
            swatch.Reset();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
    }
}