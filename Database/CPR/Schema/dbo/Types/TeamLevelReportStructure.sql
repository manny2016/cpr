CREATE TYPE [dbo].[TeamLevelReportStructure] AS TABLE
(
	
	[Monthly] NVARCHAR(50) NOT NULL,
	[Weekly] NVARCHAR(50) NOT NULL,
	[IsTotalLine] BIT,
	[BatchJobId] NVARCHAR(50) NOT NULL,
	[Metadata] NVARCHAR(MAX)
		
)
