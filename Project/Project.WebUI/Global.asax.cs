using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Project.WebUI.Infrastructure.Binders;

namespace Project.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof (double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof (double?), new DoubleModelBinder());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var exception = ex as HttpException;
            var httpCode = exception?.GetHttpCode() ?? 0;
            //switch (httpCode)
            //{
            //    case 404:
            //        Response.Redirect("/Error/NotFound");
            //        break;
            //    case 403:
            //        Response.Redirect("/Error/BadRequest");
            //        break;
            //    case 500:
            //        Response.Redirect("/Error/InternalServerError");
            //        break;
            //    default:
            //        Response.Redirect("/Error/InternalServerError");
            //        break;
            //}
        }
    }
}