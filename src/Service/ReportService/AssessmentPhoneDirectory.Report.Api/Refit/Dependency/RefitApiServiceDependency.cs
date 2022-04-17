using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AssessmentPhoneDirectory.Report.Api;
using Refit;
using System;
using AssessmentPhoneDirectory.Report.Api.Refit.Interfaces;

namespace AssessmentPhoneDirectory.Report.Api
{
    public class RefitApiServiceDependency
    {

        #region fields

        private static RefitApiServiceDependency shared;
        private static object obj = new object();

        #endregion


        internal static IServiceCollection Services { get; set; }
        internal static IServiceProvider ServiceProvider { get; private set; }
        internal static IConfiguration Configuration { get; set; }

        private static RefitApiServiceDependency Shared
        {
            get
            {
                if (shared == null)
                {
                    lock (obj)
                    {
                        if (shared == null)
                        {
                            shared = new RefitApiServiceDependency();
                        }
                    }
                }

                return shared;
            }
        }



        private IContactApi _ContactApi { get => ServiceProvider.GetRequiredService<IContactApi>(); }
        private IQueueApi _QueueApi { get => ServiceProvider.GetRequiredService<IQueueApi>(); }

        //Exposed public static props via RefitApiServiceDependency.Shared 
        public static IContactApi ContactApi { get => Shared._ContactApi; }
        public static IQueueApi QueueApi { get => Shared._QueueApi; }





        private RefitApiServiceDependency()
        {
            if (Services == null)
                Services = new ServiceCollection();

            Init();
        }

        private void Init()
        {
            ConfigureServices(Services);
            ServiceProvider = Services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<AuthTokenHandler>();
            services.AddHttpContextAccessor();
            services.AddSingleton<AppSettings>();


            // APP SETTINGS
            var configBuilder = new ConfigurationBuilder().AddJsonFile("FactorySettings.json", optional: true);
            var config = configBuilder.Build();
            services.Configure<AppSettings>(config);

            var baseaddress = new Uri("https://localhost:44370/api");
            
            services.AddRefitClient<IContactApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = baseaddress;
            });
            //.AddHttpMessageHandler<AuthTokenHandler>()
            //.AddTransientHttpErrorPolicy(p => p.RetryAsync());

            services.AddRefitClient<IQueueApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44359/api");
            });

        }

    }
}
