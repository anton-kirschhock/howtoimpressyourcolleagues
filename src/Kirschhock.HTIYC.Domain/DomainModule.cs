using System;

using Kirschhock.HTIYC.Domain.DomainEvents;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Domain
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceDescriptors)
        {
            return serviceDescriptors;
        }
    }
}