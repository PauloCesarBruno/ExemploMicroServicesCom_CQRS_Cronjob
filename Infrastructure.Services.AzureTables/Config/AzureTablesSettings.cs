using Azul.Framework.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure.Services.AzureTables.Config
{
    public class AzureTablesSettings : AppSetting
    {
        public static AzureTablesSettings Settings
        {
            get
            {
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json"))
                    return AppFileConfiguration<AzureTablesSettings>
                                .GetSettingsFromDifferentFile($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json");
                return null;
            }
        }
        [JsonProperty("dataTableSettings")]
        public IEnumerable<DataTableParameters> Parameters { get; set; }

        public static string GetPartition(string id) => Settings.Parameters.FirstOrDefault(p => p.Id.Equals(id)).Partition;
        public static string GetConnectionString(string id) => Settings.Parameters.FirstOrDefault(p => p.Id.Equals(id)).ConnectionString;
        public static string GetTable(string id) => Settings.Parameters.FirstOrDefault(p => p.Id.Equals(id)).Table;
    }
    public class DataTableParameters
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }
        [JsonProperty("partition")]
        public string Partition { get; set; }
    }
}
