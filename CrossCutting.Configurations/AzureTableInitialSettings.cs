using Azul.Framework.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CrossCutting.Configurations
{
    public class AzureTableInitialSettings : AppSetting
    {
        public static AzureTableInitialSettings Settings
        {
            get
            {
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json"))
                    return AppFileConfiguration<AzureTableInitialSettings>
                                .GetSettingsFromDifferentFile($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json");
                return null;
            }
        }

        [JsonProperty("AzureTableInitialSettings")]
        public AzureTableInitialParameters Parameters { get; set; }
    }

    public class AzureTableInitialParameters
    {

        [JsonProperty("startDateOnUTC")]
        public string StartDateOnUTC { private get; set; }

        public string GetStartDateOnUTC() => StartDateOnUTC.EndsWith('Z') ? StartDateOnUTC : StartDateOnUTC + "Z";

    }
}
