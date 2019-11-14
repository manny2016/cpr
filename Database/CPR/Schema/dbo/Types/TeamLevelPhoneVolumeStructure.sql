CREATE TYPE [dbo].[TeamLevelPhoneVolumeStructure] AS TABLE
(
	[CreatedDateTime]	BIGINT,
	[Channel]			NVARCHAR(50) NOT NULL,	
	[Program]			NVARCHAR(50) NOT NULL,
	[Region]			NVARCHAR(50) NOT NULL,
	[Market]			NVARCHAR(50) NOT NULL,
	[Supplier]			NVARCHAR(50),
	[Offered]			INT NOT NULL,
	[Handled]			INT NOT NULL,
	[BatchJobId]		NVARCHAR(50) NOT NULL
	
)
