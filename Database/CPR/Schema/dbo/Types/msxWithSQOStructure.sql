CREATE TYPE [dbo].[msxWithSQOStructure] AS TABLE
(
	[OpportunityId]		NVARCHAR(50),
	[BatchJobId]			NVARCHAR(50) NOT NULL,
	[Agent]				NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Status]			NVARCHAR(50),
	[EstRevenue]		Money,
	[Currency]			NVARCHAR(50),	
	[SourceCampaign]	NVARCHAR(200),
	[CreatedDateTime]	BIGINT
)
