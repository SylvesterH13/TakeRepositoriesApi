using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;
using TakeRepositoriesApi.Services;

namespace TakeRepositoriesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var apiKey = "ApiKey";
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TakeRepositoriesApi", Version = "v1" });
                c.AddSecurityDefinition(apiKey, new OpenApiSecurityScheme
                {
                    Description = "Api key needed to access the endpoints. ApiKey: My_API_Key",
                    In = ParameterLocation.Header,
                    Name = apiKey,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = apiKey,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = apiKey
                            },
                         },
                         new string[] {}
                     }
                });
            });

            services.AddScoped<IHostingSoftwareService, GithubService>(sp =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(Configuration["HostingSoftwareServiceBaseUri:Github"])
                };
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                return new GithubService(httpClient);
            });

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TakeRepositoriesApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
