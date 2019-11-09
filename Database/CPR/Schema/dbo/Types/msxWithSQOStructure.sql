CREATE TYPE [dbo].[msxWithSQOStructure] AS TABLE
(
	[OpportunityId]		NVARCHAR(50),
	[BatchJobId]		BIGINT NOT NULL, 			
	[Agent]				NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Status]			NVARCHAR(50),
	[EstRevenue]		Money,
	[Currency]			NVARCHAR(50),	
	[CreatedDateTime]	BIGINT
)
