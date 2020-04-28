using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InsuranceInc.WebApi.Extensions;
using InsuranceInc.Business.Helpers;
using InsuranceInc.Business.Services;
using InsuranceInc.Data.Services;

namespace InsuranceInc.WebApi
{
    //
    // The startup class configures the request pipeline of the application and how all requests are handled.
    //
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddMemoryCache();

            // Configure Basic Authentication.
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Configure DI for application services.
            services.AddScoped<IUserService, UserService>();

            // e.g. scoped to a particular http request; for one single request it hands out the same instance of ClientService.
            services.AddScoped<IClientService, ClientService>();
            services.AddHttpClient<IClientData, ApiClientData>(client =>
            {
                client.BaseAddress = new Uri(Configuration["WebService:ClientsBaseUri"]);
            });

            services.AddScoped<IPolicyService, PolicyService>();
            services.AddHttpClient<IPolicyData, ApiPolicyData>(client =>
            {
                client.BaseAddress = new Uri(Configuration["WebService:PoliciesBaseUri"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();

            // Global cors policy.
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
