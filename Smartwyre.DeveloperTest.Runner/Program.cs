using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Implementations;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Implementations;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Types.Readers;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddScoped<IDbConnection>(provider =>
        {
            // We'd read our IConfiguration to get our connection string and use a connection pool
            // we could also implement further management of our connections and repositories using the Unit of Work pattern
            var connection = new NpgsqlConnection("User ID=postgres;Password=Potato123;Host=localhost;Port=5432;Database=smartwyre_test;");

            return connection;
        });

        // We register our dependencies
        builder.Services.AddScoped<IProductDataStore, ProductDataStore>();
        builder.Services.AddScoped<IRebateDataStore, RebateDataStore>();
        builder.Services.AddScoped<IRebateService, RebateService>();

        using var host = builder.Build();

        var request = new CalculateRebateRequestReader().Read().Request;

        await HandleRebateRequestAsync(host.Services, request);

        Console.ReadKey();
    }

    static async Task HandleRebateRequestAsync(IServiceProvider hostProvider, CalculateRebateRequest request)
    {
        using IServiceScope serviceScope = hostProvider.CreateScope();

        var services = serviceScope.ServiceProvider;

        var rebateService = services.GetRequiredService<IRebateService>();

        var result = await rebateService.CalculateAsync(request);

        Console.WriteLine($"RebateCalculationStatus={result.Success}");
    }
}
