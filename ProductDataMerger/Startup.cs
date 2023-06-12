using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductDataMerger.Classes;
using ProductDataMerger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager = ProductDataMerger.Classes.ConfigurationManager;

namespace ProductDataMerger
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        public Startup() 
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddTransient<ITokenHandler, TokenHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IApiRequest, ApiRequest>();
            services.AddTransient<IRestClientFactory, RestClientFactory>();
            services.AddTransient<IRestRequestFactory, RestRequestFactory>();
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
        }
    }
}
