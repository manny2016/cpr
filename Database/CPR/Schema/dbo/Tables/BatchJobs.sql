CREATE TABLE [dbo].[BatchJobs]
(
	[Id]					BIGINT NOT NULL  IDENTITY(1, 1) PRIMARY KEY,
	[Agent]					NVARCHAR(50),
	[FileName]				NVARCHAR(100),
	[MD5]					NVARCHAR(100),
	[CreatedDateTime]		BIGINT
)
