CREATE TABLE [dbo].[TeamLevelPhoneVolume]
(
	[CreatedDateTime]	BIGINT,
	[Channel]			NVARCHAR(50) NOT NULL,	
	[Program]			NVARCHAR(50) NOT NULL,
	[Region]			NVARCHAR(50) NOT NULL,
	[Market]			NVARCHAR(50) NOT NULL,
	[Supplier]			NVARCHAR(50),
	[Offered]			INT NOT NULL,
	[Handled]			INT NOT NULL,
	[BatchJobId]		NVARCHAR(50) NOT NULL
		CONSTRAINT [FK_PhoneVolume_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),	
	
	[Version]			INT NOT NULL,
	[CreatedMonthly]	AS FORMAT(DATEADD(S,[CreatedDateTime] / 86400 * 86400,'1970-01-01'),'yyyyMM'),
	[CreatedWeekNo]		AS 'Week ' + CONVERT(NVARCHAR(2),(DATEPART(WEEK,DATEADD(S, [CreatedDateTime] / 86400*86400, '1970-01-01')) % 5 +1)),
	[CreatedDaily]		AS DATEADD(S, [CreatedDateTime] / 86400 * 86400, '1970-01-01')	
		CONSTRAINT [PK_PhoneVolume] PRIMARY KEY CLUSTERED ([Channel],[Program],[Region],[Market],[Supplier],[CreatedDateTime])
)
