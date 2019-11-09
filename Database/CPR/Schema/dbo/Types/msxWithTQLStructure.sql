CREATE TYPE [dbo].[msxWithTQLStructure] AS TABLE
(
	[LeadId]			NVARCHAR(50),
	[BatchJobId]		BIGINT NOT NULL,			
	[Agent]				NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Status]			NVARCHAR(50),
	[EstValue]			Money,
	[Currency]			NVARCHAR(50),
	[CSSDispostion]		NVARCHAR(50),
	[CreatedDateTime]	BIGINT
)
