using AssessmentPhoneDirectory.Contact.Infrastructure.Models;
using AssessmentPhoneDirectory.Contact.Manager.Abstract;
using AssessmentPhoneDirectory.Contact.Manager.Concrete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "AssessmentPhoneDirectory.Contact.Api",
                        Version = "v1",
                        Description = "PhoneDirectory Services",
                        Contact = new OpenApiContact
                        {
                            Name = "Oðuzhan Yýkýlmaz",
                            Email = "oguzhanyklmz27@gmail.com"
                        }
                    });
                c.CustomSchemaIds(x => x.FullName);
            });
            services.AddScoped<IContactManager, ContactManager>();
            services.AddScoped<IContactInfoManager, ContactInfoManager>();

            services.AddSingleton<MongoDBContext>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var conf = new RedisConfiguration
            {
                ConnectionString = configuration.GetConnectionString("Redis"),
                ServerEnumerationStrategy = new ServerEnumerationStrategy
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(conf);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssessmentPhoneDirectory.Contact.Api v1"));
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
