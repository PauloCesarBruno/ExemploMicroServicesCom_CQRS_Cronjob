using Infrastructure.Services.AzureTables.Models;
using System;
using System.Globalization;

namespace Infrastructure.Services.Contracts
{
    public class DeltaModel : BaseAzureTablesModel
    {
        public string StartDate { get; set; }

        public DateTime GetParsedStartDate() => DateTime.Parse(StartDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

        public void UpdateStartDate(DateTime dateTime) => StartDate = dateTime.ToString("u");
    }
}