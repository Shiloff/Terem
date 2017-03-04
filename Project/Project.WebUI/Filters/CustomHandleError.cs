using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.WebUI.Infrastructure.Core;
namespace Project.WebUI.Filters
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
           Log.Logger.Error("{0}" + System.Environment.NewLine + "{1}", filterContext.Exception.Message, filterContext.Exception.StackTrace);
        }
    }
}