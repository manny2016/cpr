CREATE TYPE [dbo].[ChatStructure] AS TABLE
(
	[SkillSet]			NVARCHAR(50) NOT NULL,
	[Agent]				NVARCHAR(50) NOT NULL,
	[CreatedDateTime]	BIGINT	NOT NULL,
	BatchJobId			BIGINT NOT NULL,		
	[Channel]			NVARCHAR(50),	
	[Country]			NVARCHAR(50),
	[Volume]			INT	
)
