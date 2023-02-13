using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Queries
{
    public class PartNumberQuery
    {
        public static string GetAllPartNumber(DateTime? date)
        {
            string dateReference = date is null ? 
                "sysdate-300" : 
                $"to_date('{date.Value.ToString("yyyy-MM-dd HH:mm:ss")}','YYYY-MM-DD HH24:mi:ss')";

            return PartNumberQuery.ParNumberQueryString.Replace("__DATE_REFERENCE_SCRIPT__", dateReference);
        }

        public static string GetAllPartNumberQuantity(DateTime? date)
        {
            string dateReference = date is null ?
                "sysdate-300" :
                $"to_date('{date.Value.ToString("yyyy-MM-dd HH:mm:ss")}','YYYY-MM-DD HH24:mi:ss')";

            return PartNumberQuery.ParNumberQueryString.Replace("__DATE_REFERENCE_SCRIPT__", dateReference);
        }

        private static string ParNumberQueryString = @$"SELECT PNI.PN_INTERCHANGEABLE,
                                                                PNM.PN,
                                                                PNM.PN_DESCRIPTION,
                                                                PNM.CATEGORY,
                                                                PNM.STOCK_UOM,
                                                                PNM.HAZARDOUS_MATERIAL,
                                                                PNM.STATUS,
                                                                PNM.SHELF_LIFE_FLAG,
                                                                PNM.SHELF_LIFE_DAYS,
                                                                PNM.CHAPTER,
                                                                PNM.SECTION,
                                                                PNM.AVERAGE_COST,
                                                                PNM.STANDARD_COST,
                                                                PNM.SECONDARY_COST,
                                                                PNM.TOOL_CALIBRATION_FLAG,
                                                                PNM.TOOL_LIFE_DAYS,
                                                                PNM.HAZARDOUS_MATERIAL_NO,
                                                                PNM.TOOL_CONTROL_ITEM,
                                                                PNM.INVENTORY_TYPE,
                                                                PNM.CREATED_DATE,
                                                                PNM.MODIFIED_DATE 
                                                            FROM ODB.PN_MASTER PNM,
                                                                ODB.PN_INTERCHANGEABLE PNI
                                                        WHERE PNM.PN = PNI.PN
                                                            AND PNM.STATUS = 'ACTIVE'
                                                            AND PNM.MODIFIED_DATE >= __DATE_REFERENCE_SCRIPT__";


        public static string PartNumberQuantity = @"SELECT PID.LOCATION,
                                                           PNM.PN,
                                                           PID.SN,
                                                           PNI.PN_INTERCHANGEABLE,
                                                           PNM.PN_DESCRIPTION,
                                                           PNM.CATEGORY,
                                                           PNM.VENDOR,
                                                           PID.QTY_AVAILABLE,
                                                           PID.QTY_RESERVED,
                                                           PID.QTY_IN_TRANSFER,
                                                           PID.QTY_PENDING_RI,
                                                           PID.QTY_US,
                                                           PID.QTY_IN_REPAIR,
                                                           PID.CREATED_DATE,
                                                           PID.MODIFIED_DATE
                                                      FROM ODB.PN_INVENTORY_DETAIL PID,
                                                           ODB.PN_INTERCHANGEABLE PNI,
                                                           ODB.PN_MASTER PNM
                                                     WHERE PNM.PN = PNI.PN
                                                       AND PID.PN = PNI.PN_INTERCHANGEABLE
                                                       AND PNM.STATUS = 'ACTIVE'
                                                       AND PNM.STATUS = PNI.STATUS
                                                       AND NOT PID.LOCATION IS NULL
                                                       AND (PID.MODIFIED_DATE >= __DATE_REFERENCE_SCRIPT__ OR PNI.MODIFIED_DATE >= __DATE_REFERENCE_SCRIPT__ OR PNM .MODIFIED_DATE >= __DATE_REFERENCE_SCRIPT__)";
    }
}

