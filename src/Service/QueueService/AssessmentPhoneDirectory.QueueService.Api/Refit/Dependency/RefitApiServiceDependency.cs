using AssessmentPhoneDirectory.QueueService.Api.Refit.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace AssessmentPhoneDirectory.QueueService.Api
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



        private IJobApi _JobApi { get => ServiceProvider.GetRequiredService<IJobApi>(); }

        //Exposed public static props via RefitApiServiceDependency.Shared 
        public static IJobApi JobApi { get => Shared._JobApi; }





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

            services.AddRefitClient<IJobApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44395/api");
            });

        }

    }
}
