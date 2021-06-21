using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Kirschhock.HTIYC.Security
{
    public static class SecurityModule
    {
        public static IServiceCollection AddSecurity(this IServiceCollection serviceDescriptors,
                                                     Action<SecurityConfiguration> configureSecurity)
        {
            var securityConfig = new SecurityConfiguration();
            configureSecurity(securityConfig);

            serviceDescriptors.AddAuthentication(opt =>
            {
                opt.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddMicrosoftIdentityWebApp(opt =>
            {
                opt.Instance = "https://login.microsoftonline.com/";
                opt.CallbackPath = "/sign-in";
                opt.SignedOutCallbackPath = "/sign-out";
                opt.AccessDeniedPath = "/Nothing-Here";
                //opt.Scope.Add("openid");
                //opt.Scope.Add("profile");
                //opt.Scope.Add("email");
                //opt.Scope.Add("roles");
                opt.ClientId = securityConfig.ClientId;
                opt.ClientSecret = securityConfig.ClientSecret;
                opt.TenantId = securityConfig.TenantId;
                opt.ResponseType = "code";
                opt.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = (ctx) =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });

            serviceDescriptors.AddAuthorization(opt =>
            {
                opt.AddPolicy(GlobalConstants.Policies.Admin, opt =>
                {
                    opt.RequireClaim(ClaimTypes.Role, "admin");
                });
            });

            return serviceDescriptors;
        }
    }
}
