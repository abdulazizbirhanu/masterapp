using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DataAPI.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace DataAPI.Config
{
    public static class ServiceRegistery
    {
        /// <summary>
        /// Application specific service registry
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();
            services.Configure<DataConfig>(config.GetSection("DataConfig"));
            services.Configure<ApplicationInsightsOption>(config.GetSection("ApplicationInsights"));
            services.AddAppInsight();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new QueryStringApiVersionReader();
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionSelector = new LowestImplementedApiVersionSelector(config);
            });
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());


            services.AddRazorPages();
            services.AddMvc(mvc =>
            {
                mvc.Conventions.Add(new DefaultRoutePrefixs());

            }).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegateHandler>();

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("authstore:/data/key"));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "scopes.sample.api";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddAppInsight(this IServiceCollection services)
        {
            var applicationInsightsOptions = services.BuildServiceProvider().GetService<IOptions<ApplicationInsightsOption>>().Value;
            services.AddApplicationInsightsTelemetry(applicationInsightsOptions.InstrumentationKey);
            services.AddApplicationInsightsKubernetesEnricher();
            services.AddApplicationInsightsTelemetryProcessor<HealthProbeTelemetryProcessor>();

            return services;
        }
    }

    public class HealthProbeTelemetryProcessor : ITelemetryProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITelemetryProcessor _nextProcessor;
        public static string HealthProbeHeaderName => "HealthProbe-Type";

        public HealthProbeTelemetryProcessor(IHttpContextAccessor httpContextAccessor, ITelemetryProcessor nextProcessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _nextProcessor = nextProcessor;
        }

        public void Process(ITelemetry item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!string.IsNullOrWhiteSpace(item.Context.Operation.SyntheticSource))
                return;

            var isNotRequestTelemetry = !(item is RequestTelemetry);

            if ((isNotRequestTelemetry || _httpContextAccessor.HttpContext == null || !(_httpContextAccessor.HttpContext.Request?.Headers.ContainsKey(HealthProbeHeaderName)).GetValueOrDefault()))
                _nextProcessor.Process(item);
        }
    }
    public class DefaultRoutePrefixs : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var applicationController in application.Controllers)
            {
                applicationController.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = "api/[controller]"
                    }
                });
            }
        }
    }
}
