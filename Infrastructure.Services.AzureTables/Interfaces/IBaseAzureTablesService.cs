using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.AzureTables.Interfaces
{
    public interface IBaseAzureTablesService<T> where T : IBaseAzureTablesModel
    {
        public IEnumerable<T> GetRowsFromPartitionKey();
        public Task<bool> UpsertEntity(string rowKey);

        public Task<bool> UpsertEntity(T model);
    }
}
