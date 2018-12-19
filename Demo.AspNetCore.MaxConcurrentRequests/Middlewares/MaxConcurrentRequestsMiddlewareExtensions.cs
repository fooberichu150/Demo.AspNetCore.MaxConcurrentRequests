﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.AspNetCore.MaxConcurrentRequests.Middlewares
{
    public static class MaxConcurrentRequestsMiddlewareExtensions
    {
        public static IApplicationBuilder UseMaxConcurrentRequests(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

			var throttlingSettings = app.ApplicationServices.GetService<Microsoft.Extensions.Options.IOptions<MaxConcurrentRequestsOptions>>();
			if (!throttlingSettings?.Value.Enabled ?? false)
				return app;

			return app.UseMiddleware<MaxConcurrentRequestsMiddleware>();
        }

		public static IServiceCollection ConfigureRequestThrottleServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MaxConcurrentRequestsOptions>(configuration.GetSection("MaxConcurrentRequests"));

			return services;
		}
	}
}
