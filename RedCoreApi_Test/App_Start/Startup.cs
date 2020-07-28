using System;  
using Microsoft.Owin;  
using Owin;  
using Microsoft.Owin.Security.OAuth;  
using System.Web.Http;
using RedCoreApi_Test.Models;
using System.Threading.Tasks;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(RedCoreApi_Test.App_Start.Startup))]

namespace RedCoreApi_Test.App_Start
{
    public class Startup
    {
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Configuration(IAppBuilder app)
        {
            //app.us
            //app.
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {

                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new AuthorizationServerProvider()
            };
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "http://localhost" })
        //        }
    }
}