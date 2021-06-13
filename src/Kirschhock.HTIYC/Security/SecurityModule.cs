using System;

using Kirschhock.HTIYC.Common;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Kirschhock.HTIYC.Security
{
    public static class SecurityModule
    {
        public static IServiceCollection AddSecurity(this IServiceCollection serviceDescriptors,
                                                     Action<SecurityConfiguration> configureSecurity)
        {
            serviceDescriptors.AddAuthentication(opt =>
            {
            }).AddMicrosoftIdentityWebApp(opt =>
            {
                // TODO configure AAD 
            });

            serviceDescriptors.AddAuthorization(opt =>
            {
                opt.AddPolicy(GlobalConstants.Policies.Admin, opt =>
                {
                    //opt.RequireClaim() // TODO - Add Role in Azure
                });
            });

            return serviceDescriptors;
        }
    }
}
