﻿CREATE TABLE [dbo].[msxWithTQL]
(
	[LeadId]			NVARCHAR(50),
	[BatchJobId]		NVARCHAR(50) NOT NULL
		CONSTRAINT [FK_msxWithTQL_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),	
	[Agent]				NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Status]			NVARCHAR(50),
	[EstValue]			Money,
	[Currency]			NVARCHAR(50),
	[CSSDispostion]		NVARCHAR(200),
	[SourceCampaign]	NVARCHAR(200),
	[CreatedDateTime]	BIGINT,
	[Version]			INT,	
	[CreatedMonthly]	AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy'),
	[CreatedWeekNo]		AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'MMM yyyy') + ' Week ' + CONVERT(NVARCHAR(2),(DATEPART(WEEK,DATEADD(S, [CreatedDateTime] / 86400*86400, '1970-01-01')) % 5 +1)),
	[CreatedDaily]		AS DATEADD(S, [CreatedDateTime] / 86400 * 86400, '1970-01-01')	
		CONSTRAINT [PK_msxTQL] PRIMARY KEY CLUSTERED ([LeadId])
	
)
