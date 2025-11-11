using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoadStatus.ApiServices;
using RoadStatus.Services.ServiceInterfaces;

namespace RoadStatus
{
    internal class Program
    {
        //retrieves and displays the status of a specified road by calling the TfL API
        static async Task<int> Main(string[] args)
           {
                if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
                {
                    Console.WriteLine("Please provide a valid road ID");
                    return 1;
                }

                var roadId = args[0];

                try
                {
                    var host = CreateHostBuilder(args).Build();
                    var apiService = host.Services.GetRequiredService<ITflApiService>();
                    var checker = new RoadStatusChecker(apiService);

                    var result = await checker.CheckStatus(roadId);

                    if (result.IsValid)
                    {
                        Console.WriteLine($"The status of the {result.RoadDisplayName} is as follows");
                        //Console.WriteLine($"Road Status is {result.RoadStatus}");
                        Console.WriteLine($"Road Status is {result.RoadStatus}");
                        Console.WriteLine($"Road Status Description is {result.RoadStatusDescription}");
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine(result.ErrorMessage);
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Application error: {ex.Message}");
                    return 1;
                }
            }

            static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services.AddHttpClient<ITflApiService, TflApiService>();
                    });
        }
    
}
