CREATE TYPE [dbo].[msxWithTQLStructure] AS TABLE
(
	[LeadId]			NVARCHAR(50),
	[BatchJobId]		NVARCHAR(50) NOT NULL,			
	[Agent]				NVARCHAR(100),
	[Country]			NVARCHAR(100),
	[Status]			NVARCHAR(50),
	[EstValue]			Money,
	[Currency]			NVARCHAR(50),
	[CSSDispostion]		NVARCHAR(200),
	[SourceCampaign]	NVARCHAR(200),
	[CreatedDateTime]	BIGINT
)
