using System;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Project.WebUI.Models;

namespace Project.WebUI
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(EFDBContext.Create);
            app.CreatePerOwinContext<ApplicationManager>(ApplicationManager.Create);            
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationManager, ApplicationUserEntity>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
           
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "1",
            //    clientSecret: "1");

            //app.UseTwitterAuthentication(
            //   consumerKey: "1",
            //   consumerSecret: "1");

            app.UseVkontakteAuthentication(
                appId: "5156845",
                appSecret: "8Hemd2abJ16j2AkfAnTd",
                scope: "friends,messages"
                );

            app.UseFacebookAuthentication(
               appId: "1503375426626051",
               appSecret: "fac92f5822659cc8c53fbd7d78ace976");

            ////app.UseGoogleAuthentication();
            app.UseGoogleAuthentication(
                 clientId: "270937924175-hofbao9g229vg3kfo5q3d9cf2a7ncct1.apps.googleusercontent.com",
                 clientSecret: "jEH8nEyOiOQnZv73Axk9O9e5");
        }
    }
}