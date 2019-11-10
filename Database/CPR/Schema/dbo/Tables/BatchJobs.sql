CREATE TABLE [dbo].[BatchJobs]
(
	[Id]					NVARCHAR(50) PRIMARY KEY,
	[Agent]					NVARCHAR(50),
	[CreatedDateTime]		BIGINT,
	[AttachInfo]			NVARCHAR(MAX),
	[Metadata]				NVARCHAR(MAX)		
)
