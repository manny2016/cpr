CREATE TABLE [dbo].[TeamLevelReport]
(
	[Monthly] NVARCHAR(50) NOT NULL,
	[Weekly] NVARCHAR(50) NOT NULL,
	[IsTotalLine] BIT,
	[BatchJobId] NVARCHAR(50) NOT NULL
		CONSTRAINT [FK_TeamLevelReport_BatchJobs_Id] FOREIGN KEY REFERENCES [dbo].[BatchJobs] (Id),		
	[Version] INT NOT NULL,
	[Metadata] NVARCHAR(MAX),
		CONSTRAINT [PK_TeamLevel] PRIMARY KEY CLUSTERED ([Monthly],[Weekly])
)
