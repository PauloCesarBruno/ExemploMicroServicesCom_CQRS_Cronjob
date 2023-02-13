using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Infrastructure.Services.AzureTables.Config
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterTableClient(this IServiceCollection services, string serviceId)
        {
            var azureTablesSettings = AzureTablesSettings.Settings.Parameters.FirstOrDefault(p => p.Id.Equals(serviceId));
            services.AddSingleton(new TableClient(azureTablesSettings.ConnectionString, azureTablesSettings.Table));
        }
    }
}
