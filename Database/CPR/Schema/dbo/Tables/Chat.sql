CREATE TABLE [dbo].[Chat]
(
	[SkillSet]			NVARCHAR(50) NOT NULL,
	[Agent]				NVARCHAR(50) NOT NULL,
	[CreatedDateTime]	BIGINT	NOT NULL,
	[BatchJobId]		NVARCHAR(50) NOT NULL
		CONSTRAINT [FK_Chat_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),			
	[Version]			INT,	
	[CreatedMonthly]	AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy'),
	[CreatedWeekNo]		AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy') + ' Week ' + CONVERT(NVARCHAR(2),(DATEPART(WEEK,DATEADD(S, [CreatedDateTime] / 86400*86400, '1970-01-01')) % 5 +1)),
	[CreatedDaily]		AS DATEADD(S, [CreatedDateTime] / 86400 * 86400, '1970-01-01')	
		CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED ([SkillSet],[Agent],[CreatedDateTime])
)
