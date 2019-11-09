using CPR.Data.Import.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CPR.Data.Import
{
    public static class Constants
    {
        public static readonly SqlMetaData[] AvayaStructure = new SqlMetaData[]
        {
            new SqlMetaData("SkillSet",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent]",SqlDbType.NVarChar,50) ,
            new SqlMetaData("CreatedDateTime",SqlDbType.BigInt),
            new SqlMetaData("BatchJobId",SqlDbType.BigInt),
            new SqlMetaData("Channel",SqlDbType.NVarChar,50),
            new SqlMetaData("Country",SqlDbType.NVarChar,50),
            new SqlMetaData("Volume",SqlDbType.Int)
        };
        public static readonly SqlMetaData[] ChatStructure = new SqlMetaData[] {
            new SqlMetaData("SkillSet",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent]",SqlDbType.NVarChar,50) ,
            new SqlMetaData("CreatedDateTime",SqlDbType.BigInt),
            new SqlMetaData("BatchJobId",SqlDbType.BigInt),
            new SqlMetaData("Channel",SqlDbType.NVarChar,50),
            new SqlMetaData("Country",SqlDbType.NVarChar,50),
            new SqlMetaData("Volume",SqlDbType.Int)
        };
        public static readonly SqlMetaData[] msxWithSQOStructure = new SqlMetaData[] {
            new SqlMetaData("BatchJobId",SqlDbType.BigInt),
            new SqlMetaData("OpportunityId",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent",SqlDbType.NVarChar,50),
            new SqlMetaData("Country",SqlDbType.NVarChar,50),
            new SqlMetaData("Status",SqlDbType.NVarChar,50),
            new SqlMetaData("EstRevenue" ,SqlDbType.Money),
            new SqlMetaData("Currency",SqlDbType.NVarChar,50),
            new SqlMetaData("CreatedDateTime" ,SqlDbType.BigInt )
        };
        public static readonly SqlMetaData[] msxWithTQLStructure = new SqlMetaData[] {
            new SqlMetaData("BatchJobId",SqlDbType.BigInt),
            new SqlMetaData("LeadId",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent",SqlDbType.NVarChar,50),
            new SqlMetaData("Status",SqlDbType.NVarChar,50),
            new SqlMetaData("EstValue" ,SqlDbType.Money),
            new SqlMetaData("Currency",SqlDbType.NVarChar,50),
            new SqlMetaData("CSSDispostion",SqlDbType.NVarChar,50),
            new SqlMetaData("CreatedDateTime" ,SqlDbType.BigInt )    
        };
    }
}
