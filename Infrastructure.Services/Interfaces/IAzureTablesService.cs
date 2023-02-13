using Infrastructure.Services.AzureTables.Interfaces;
using Infrastructure.Services.AzureTables.Services;
using Infrastructure.Services.Contracts;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IAzureTablesService : IBaseAzureTablesService<DeltaModel>
    {
        Task<bool> Commit();
    }
}
