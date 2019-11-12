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
            new SqlMetaData("Agent",SqlDbType.NVarChar,50) ,
            new SqlMetaData("CreatedDateTime",SqlDbType.BigInt),
            new SqlMetaData("BatchJobId",SqlDbType.NVarChar,50),
            new SqlMetaData("Volume",SqlDbType.Int)
        };
        public static readonly SqlMetaData[] ChatStructure = new SqlMetaData[] {
            new SqlMetaData("SkillSet",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent",SqlDbType.NVarChar,50) ,
            new SqlMetaData("CreatedDateTime",SqlDbType.BigInt),
            new SqlMetaData("BatchJobId",SqlDbType.NVarChar,50)

        };
        public static readonly SqlMetaData[] msxWithSQOStructure = new SqlMetaData[] {
            new SqlMetaData("OpportunityId",SqlDbType.NVarChar,50),
            new SqlMetaData("BatchJobId",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent",SqlDbType.NVarChar,50),
            new SqlMetaData("Country",SqlDbType.NVarChar,50),
            new SqlMetaData("Status",SqlDbType.NVarChar,50),
            new SqlMetaData("EstRevenue" ,SqlDbType.Money),
            new SqlMetaData("Currency",SqlDbType.NVarChar,50),
            new SqlMetaData("SourceCampaign",SqlDbType.NVarChar,200),
            new SqlMetaData("CreatedDateTime" ,SqlDbType.BigInt )
        };
        public static readonly SqlMetaData[] msxWithTQLStructure = new SqlMetaData[] {
            new SqlMetaData("LeadId",SqlDbType.NVarChar,50),
            new SqlMetaData("BatchJobId",SqlDbType.NVarChar,50),
            new SqlMetaData("Agent",SqlDbType.NVarChar,50),
            new SqlMetaData("Country",SqlDbType.NVarChar,50),
            new SqlMetaData("Status",SqlDbType.NVarChar,50),
            new SqlMetaData("EstValue" ,SqlDbType.Money),
            new SqlMetaData("Currency",SqlDbType.NVarChar,50),
            new SqlMetaData("CSSDispostion",SqlDbType.NVarChar,200),
            new SqlMetaData("SourceCampaign",SqlDbType.NVarChar,200),
            new SqlMetaData("CreatedDateTime" ,SqlDbType.BigInt )
        };

        public static readonly Dictionary<DataSourceNames, string> StoredProcedureNames = new Dictionary<DataSourceNames, string>()
        {
            { DataSourceNames.Avaya,"spImportAvaya" },
            { DataSourceNames.Chat,"spImportChat" },
            { DataSourceNames.MSXSQO,"spImportmsxSQO" },
            { DataSourceNames.MSXTQL,"spImportmsxTQL" },
        };

        public static readonly Dictionary<DataSourceNames, string> StructureNames = new Dictionary<DataSourceNames, string>()
        {
            { DataSourceNames.Avaya, "[dbo].[AvayaStructure]" },
            { DataSourceNames.Chat,  "[dbo].[ChatStructure]" },
            { DataSourceNames.MSXSQO,"[dbo].[msxWithSQOStructure]" },
            { DataSourceNames.MSXTQL,"[dbo].[msxWithTQLStructure]" },
        };
        public static readonly Dictionary<DataSourceNames, SqlMetaData[]> Structures = new Dictionary<DataSourceNames, SqlMetaData[]>()
        {
            { DataSourceNames.Avaya, AvayaStructure},
            { DataSourceNames.Chat,  ChatStructure},
            { DataSourceNames.MSXSQO,msxWithSQOStructure},
            { DataSourceNames.MSXTQL,msxWithTQLStructure},
        };
        public static readonly string[] AvayaValidStrings = new string[] { "avaya" };
        public static readonly string[] ChatValidStrings = new string[] { "chat" };
        public static readonly string[] msxTQLValidStrings = new string[] { "tql" };
        public static readonly string[] msxSQOValidStrings = new string[] { "sqo" };
        public static readonly string[] DirectVolumeTeamLevel = new string[] {"team" };
        public static readonly string[] PhoneVolume = new string[] { "phone" };
    }
}
