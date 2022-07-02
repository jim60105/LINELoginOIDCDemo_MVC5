using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace LINELoginOIDCDemo_MVC5
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //IdentityModelEventSource.ShowPII = true;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://access.line.me/",

                ClientId = WebConfigurationManager.AppSettings["OpenIDConnect:ClientID"],
                ClientSecret = WebConfigurationManager.AppSettings["OpenIDConnect:ClientSecret"],

                RedirectUri = WebConfigurationManager.AppSettings["OpenIDConnect:RedirectUri"],

                RedeemCode = true,

                ResponseMode = OpenIdConnectResponseMode.Query,
                ResponseType = OpenIdConnectResponseType.Code,

                Scope = "openid profile email",

                SecurityTokenValidator = new JwtSecurityTokenHandler
                {
                    // Disable the built-in JWT claims mapping feature.
                    InboundClaimTypeMap = new Dictionary<string, string>(),

                },

                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["OpenIDConnect:ClientSecret"])),
                },

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    // Note: by default, the OIDC client throws an OpenIdConnectProtocolException
                    // when an error occurred during the authentication/authorization process.
                    // To prevent a YSOD from being displayed, the response is declared as handled.
                    AuthenticationFailed = notification =>
                    {
                        if (string.Equals(notification.ProtocolMessage.Error, "access_denied", StringComparison.Ordinal))
                        {
                            notification.HandleResponse();

                            notification.Response.Redirect("/");
                        }

                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}
