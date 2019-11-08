CREATE TABLE [dbo].[msxWithSQO]
(
	[BatchJobId] BIGINT NOT NULL 
		CONSTRAINT [FK_msxWithSQO_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),	
	[OpportunityId]	NVARCHAR(50),
	[Agent]				NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Status]			NVARCHAR(50),
	[EstRevenue]		Money,
	[Currency]			NVARCHAR(50),	
	[CreatedDateTime]	BIGINT,
	[CreatedMonthly]	AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy'),
	[CreatedWeekNo]		AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy') + ' Week ' + CONVERT(NVARCHAR(2),(DATEPART(WEEK,DATEADD(S, [CreatedDateTime] / 86400*86400, '1970-01-01')) % 4 +1)),
	[CreatedDaily]		AS DATEADD(S, [CreatedDateTime] / 86400 * 86400, '1970-01-01')	
		CONSTRAINT [PK_msxSQO] PRIMARY KEY CLUSTERED ([BatchJobId])		
)
