CREATE TABLE [dbo].[msxWithSQO]
(
	[OpportunityId]	NVARCHAR(50),
	[BatchJobId]		NVARCHAR(50) NOT NULL
		CONSTRAINT [FK_msxWithSQO_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),		
	[Agent]				NVARCHAR(100),
	[Country]			NVARCHAR(100),
	[Status]			NVARCHAR(100),
	[EstRevenue]		Money,
	[Currency]			NVARCHAR(50),	
	[CreatedDateTime]	BIGINT,
	[SourceCampaign]	NVARCHAR(200),
	[Version]			INT,	
	[CreatedMonthly]	AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy'),
	[CreatedWeekNo]		AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy') + ' Week ' + CONVERT(NVARCHAR(2),(DATEPART(WEEK,DATEADD(S, [CreatedDateTime] / 86400*86400, '1970-01-01')) % 5 +1)),
	[CreatedDaily]		AS DATEADD(S, [CreatedDateTime] / 86400 * 86400, '1970-01-01')	
		CONSTRAINT [PK_msxSQO] PRIMARY KEY CLUSTERED ([OpportunityId])		
)
