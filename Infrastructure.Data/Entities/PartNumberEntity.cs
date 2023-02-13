using Azul.Framework.Data.Entities;
using System;

namespace Infrastructure.Data.Entities
{
    public class PartNumberEntity : DataMappingBase<Guid>
    {
        public string PN_INTERCHANGEABLE { get; set; }
        public string PN { get; set; }
        public string PN_DESCRIPTION { get; set; }
        public string CATEGORY { get; set; }
        public string STOCK_UOM { get; set; }
        public string HAZARDOUS_MATERIAL { get; set; }
        public string STATUS { get; set; }
        public string SHELF_LIFE_FLAG { get; set; }
        public long? SHELF_LIFE_DAYS { get; set; }
        public string CHAPTER { get; set; }
        public string SECTION { get; set; }
        public decimal AVERAGE_COST { get; set; }
        public decimal STANDARD_COST { get; set; }
        public decimal SECONDARY_COST { get; set; }
        public string TOOL_CALIBRATION_FLAG { get; set; }
        public long? TOOL_LIFE_DAYS { get; set; }
        public string HAZARDOUS_MATERIAL_NO { get; set; }
        public string TOOL_CONTROL_ITEM { get; set; }
        public string INVENTORY_TYPE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}
