CREATE PROCEDURE [dbo].[spImportmsxTQL]
	@data [dbo].[msxWithTQLStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[msxWithTQL] AS [target]
	USING(
		SELECT 	[LeadId],[BatchJobId],[Agent],[Country],[Status],[EstValue],[Currency],[CSSDispostion],[SourceCampaign],[CreatedDateTime] FROM @data
	) AS [source]
	ON [source].[LeadId] = [target].[LeadId]
	WHEN MATCHED THEN UPDATE SET 			
		 [target].[BatchJobId] =[source].[BatchJobId]
		,[target].[Agent]=[source].[Agent]
		,[target].[Country]=[source].[Country]
		,[target].[Status]=[source].[Status]
		,[target].[EstValue]=[source].[EstValue]
		,[target].[Currency]=[source].[Currency]
		,[target].[CSSDispostion]=[source].[CSSDispostion]
		,[target].[SourceCampaign]=[source].[SourceCampaign]
		,[target].[CreatedDateTime]=[source].[CreatedDateTime]		
		,[target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([LeadId],[BatchJobId],[Agent],[Country]	,[Status]	,[EstValue]	,[Currency]	,[CSSDispostion],[SourceCampaign],[CreatedDateTime],[Version])  
		VALUES([source].[LeadId],	
			[source].[BatchJobId],	
			[source].[Agent],
			[source].[Country]	,
			[source].[Status],
			[source].[EstValue]	,
			[source].[Currency]	,
			[source].[CSSDispostion],
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
