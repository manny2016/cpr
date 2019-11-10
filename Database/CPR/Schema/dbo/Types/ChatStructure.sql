CREATE TYPE [dbo].[ChatStructure] AS TABLE
(
	[SkillSet]			NVARCHAR(50) NOT NULL,
	[Agent]				NVARCHAR(50) NOT NULL,
	[CreatedDateTime]	BIGINT	NOT NULL,
	BatchJobId			NVARCHAR(50) NOT NULL
	
	
	
)
