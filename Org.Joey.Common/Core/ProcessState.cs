using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public abstract class ProcessState<ProcessingResult> : IWorkItemState
    {
        public ProcessState(IProcessSetting<ProcessingResult> setting)
        {
            this.Setting = setting;
            this.MaxDegree = 50;
            this.BatchSize = 50;


        }
        public bool IsWaitingOnRemainingEntitiesProcessingWhenCancellationRequested { get; set; }
        public bool Synchronously { get; set; }
        public int BatchSize { get; set; }
        public long StartTime { get; set; }
        private long _LastUpdatedTime { get; set; }
        public long LastUpdatedTime
        {
            get { return this._LastUpdatedTime; }
            set { this._LastUpdatedTime = Math.Max(this._LastUpdatedTime, value); }
        }
        public int SKUWorkItemCount { get; set; }
        public bool SaveCopy { get; set; }
        public int MaxDegree { get; set; }

        public IProcessSetting<ProcessingResult> Setting { get; set; }

        public virtual string Name
        {
            get
            {
                throw new NotImplementedException("Must overwrite by child class.");
            }
        }

        public abstract IProcessingResultService<ProcessingResult> GenerateProcessingResultService();

        public void Update()
        {
            using (var database = DatabaseFactory.GenerateDatabase())
            {
                var queryString = $@"
MERGE INTO [dbo].[Schedule] [target]
USING(
	VALUES
	('{this.Name}',{this.LastUpdatedTime})
)[source]([Name],[NextTime])
ON [target].[Name]=[source].[Name]
WHEN MATCHED THEN UPDATE SET [target].[NextTime] = [source].[NextTime]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT([Name],[NextTime])
	VALUES([source].[Name],[source].[NextTime]);
";
                database.Execute(queryString);
            }
        }
    }
}
