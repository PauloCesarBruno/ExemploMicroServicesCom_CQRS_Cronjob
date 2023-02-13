namespace Infrastructure.Services.AzureTables.Interfaces
{
    public interface IBaseAzureTablesModel
    {
        string RowKey { get; set; }
        string Timestamp { get; set; }
    }
}