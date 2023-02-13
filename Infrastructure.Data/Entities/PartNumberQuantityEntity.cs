using Azul.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Entities
{
    public class PartNumberQuantityEntity : DataMappingBase<Guid>
    {
        public string LOCATION { get; set; }
        public string PN { get; set; }
        public string SN { get; set; }
        public string PN_INTERCHANGEABLE { get; set; }
        public string PN_DESCRIPTION { get; set; }
        public string CATEGORY { get; set; }
        public string VENDOR { get; set; }
        public string QTY_AVAILABLE { get; set; }
        public string QTY_RESERVED { get; set; }
        public string QTY_IN_TRANSFER { get; set; }
        public string QTY_PENDING_RI { get; set; }
        public string QTY_US { get; set; }
        public string QTY_IN_REPAIR { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}
