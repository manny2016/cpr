CREATE PROCEDURE [dbo].[spImportmsxSQO]
	@data [dbo].[msxWithSQOStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[msxWithSQO] AS [target]
	USING(
		SELECT 	[OpportunityId]	,[BatchJobId],[Agent]	,[Country]	,[Status]	,[EstRevenue]	,[Currency]	,[SourceCampaign],[CreatedDateTime] FROM @data
	) AS [source]
	ON [source].[OpportunityId] = [target].[OpportunityId]
	WHEN MATCHED THEN UPDATE SET 			
		[target].[BatchJobId] =[source].[BatchJobId]
		,[target].[Agent]=[source].[Agent]
		,[target].[Country]=[source].[Country]
		,[target].[Status]=[source].[Status]
		,[target].[EstRevenue]=[source].[EstRevenue]
		,[target].[Currency]=[source].[Currency]		
		,[target].[CreatedDateTime]=[source].[CreatedDateTime]		
,[target].[SourceCampaign]=[source].[SourceCampaign]		

		,[target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([OpportunityId],[BatchJobId]	,[Agent]	,[Country]	,[Status]	,[EstRevenue]	,[Currency]	,[SourceCampaign],[CreatedDateTime] ,[Version])  
		VALUES([source].[OpportunityId],	
			[source].[BatchJobId],	
			[source].[Agent],
			[source].[Country]	,
			[source].[Status],
			[source].[EstRevenue]	,
			[source].[Currency]	,			
			[source].[SourceCampaign],
			[source].[CreatedDateTime],	1);
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
