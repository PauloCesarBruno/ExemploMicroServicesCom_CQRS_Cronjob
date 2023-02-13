using Azure.Data.Tables;
using Infrastructure.Services.AzureTables.Config;
using Infrastructure.Services.AzureTables.Interfaces;
using Infrastructure.Services.AzureTables.Services;
using Infrastructure.Services.Contracts;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.ServicesHandler
{
    public class AzureTablesService : BaseAzureTablesService<DeltaModel>, IAzureTablesService
    {
        protected override string _partition => AzureTablesSettings.GetPartition(nameof(AzureTablesService));

        public AzureTablesService(TableClient tableClient, ILoggerFactory loggerFactory) : base(tableClient, loggerFactory)
        {
        }

        public async Task<bool> Commit()
        {
            var records = GetRowsFromPartitionKey().ToList();            
            var register = records.FirstOrDefault();
            if (register == null)
                return true;
            else
                await UpsertEntity(register);
            return true;
        }       
    }
}
