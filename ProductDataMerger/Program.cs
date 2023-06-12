using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductDataMerger.Enums;
using ProductDataMerger.Interfaces;
using ProductDataMerger.Models;
using System;
using System.Xml;

namespace ProductDataMerger
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            try
            {
                Startup startup = new Startup();
                IServiceCollection serviceCollection = new ServiceCollection();

                startup.ConfigureServices(serviceCollection);
                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
                IConfigurationRoot configurationRoot = serviceProvider.GetService<IConfigurationRoot>();
                ITokenHandler tokenHandler = serviceProvider.GetService<ITokenHandler>();
                IProductRepository productRepository = serviceProvider.GetService<IProductRepository>();

                JwtToken? jwtToken = await tokenHandler.GetAccessToken(configurationRoot);

                if (jwtToken is not null && tokenHandler.IsTokenValid(jwtToken))
                {
                    var productJsonResponse = await productRepository.GetProducts(configurationRoot, jwtToken, ProductDataTypeEnum.Json);
                    var productXmlResponse = await productRepository.GetProducts(configurationRoot, jwtToken, ProductDataTypeEnum.Xml);

                    if (productJsonResponse is not null && productXmlResponse is not null)
                    {
                        Product[] bannerProducts = productJsonResponse.Products;
                        Product[] rshughesProducts = productXmlResponse.Products;

                        for (int i = 0; i < bannerProducts.Length; i++)
                        {
                            for (int j = 0; j < rshughesProducts.Length; j++)
                            {
                                if (bannerProducts[i].Upc is null || rshughesProducts[j].Upc is null)
                                    continue;

                                if (bannerProducts[i].Upc == rshughesProducts[j].Upc)
                                {
                                    Console.WriteLine($"Banner UPC : {bannerProducts[i].Upc}, Banner ItemCode : {bannerProducts[i].ItemCode}, " +
                                        $"RsHughes UPC : {rshughesProducts[j].Upc}, RsHughes ItemCode : {rshughesProducts[j].ItemCode}");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error in fetching the JWT Token !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in Main");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}