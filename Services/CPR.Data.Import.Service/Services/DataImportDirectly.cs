
namespace CPR.Data.Import.Services
{

    using System.Collections.Generic;
    using CPR.Data.Import.Models;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    using Dapper;
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Caching.Memory;

    public class DataImportDirectly : IDataImportDirectly
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DataImportDirectly));
        private readonly IMemoryCache memoryCache;
        public DataImportDirectly(IMemoryCache cache)
        {
            this.memoryCache = cache;
        }
        public long? GetNextTime(string name = null)
        {
            var queryString = "SELECT TOP 1 NextTime FROM [dbo].[Schedule] (NOLOCK) WHERE Name=@name";
            using (var database = DatabaseFactory.GenerateDatabase())
            {   
                return database.ExecuteScalar<long?>(queryString, new { @name = name});
            }
        }


        public void Import(IEnumerable<ExcelSignleRow> records)
        {
            if (records == null || records.Count() == 0)
            {
                Logger.Info("No data need importing.");
                return;
            }
            foreach (var group in records.GroupBy(o => o.DataSourceName))
            {
                using (var database = DatabaseFactory.GenerateDatabase())
                {
                    try
                    {
                        var array = group.Select(o => Unity.Convert(o, group.Key)).Where(o => o != null);
                        if (array == null || array.Count().Equals(0))
                        {
                            continue;
                        }
                        if (database.State != ConnectionState.Open)
                            database.Open();
                        var command = database.CreateCommand();
                        command.CommandText = Constants.StoredProcedureNames[group.Key];
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 60 * 60;
                        command.Parameters.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.Structured,
                            TypeName = Constants.StructureNames[group.Key],
                            Value = group.Select(o => Unity.Convert(o, group.Key)).Where(o => o != null),
                            ParameterName = "@data"
                        });
                        var retVal = command.ExecuteNonQuery();
                        Logger.Info($"{retVal} records imported.");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message, ex);
                    }
                    finally
                    {
                        if (database.State != ConnectionState.Closed)
                            database.Close();
                    }
                }
            }
        }
        public void SaveBatchJobs(BatchJob model)
        {
            var queryString = @"
MERGE INTO [dbo].[BatchJobs] [target]
USING(
	VALUES
	(@id,@agent,@createdDateTime,@attachInfo,@metadata)
)[source]([Id],[Agent],[CreatedDateTime],[AttachInfo],[Metadata])
ON [source].[Id] = [target].[Id]
WHEN MATCHED THEN UPDATE SET 			
	[target].[Agent] = [source].[Agent],
	[target].[Metadata] = [source].[Metadata],
	[target].[AttachInfo] = [source].[AttachInfo],
	[target].[CreatedDateTime] = [source].[CreatedDateTime]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],[Agent],[CreatedDateTime],[AttachInfo],[Metadata]) 
	VALUES([source].[Id],[source].[Agent],[source].[CreatedDateTime],[source].[AttachInfo],[source].[Metadata]);
";
            using (var database = DatabaseFactory.GenerateDatabase())
            {             
                database.Execute(queryString, new
                {
                    @id = model.Id,
                    @agent = model.Agent,
                    @attachInfo = model.AttachInfo,
                    @metadata = model.Metadata,
                    @createdDateTime = model.CreatedDateTime
                });
            }
        }
        public Mapping[] GetMappings()
        {
            return this.memoryCache.GetOrCreate<Mapping[]>("cpr_mappings", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                using (var database = DatabaseFactory.GenerateDatabase())
                {
                    var queryString = "SELECT * FROM [dbo].[Mappings] (NOLOCK)";
                    return database.Query<Mapping>(queryString).ToArray();
                }
            });
        }
    }
}
