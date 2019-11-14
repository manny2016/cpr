CREATE PROCEDURE [dbo].[spImportTeamLevelReport]
	@data [dbo].[TeamLevelReportStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[TeamLevelReport] AS [target]
	USING(
		SELECT 	[Monthly],[Weekly],[IsTotalLine],[BatchJobId],[Metadata] FROM @data
	) AS [source]
	ON [source].[Monthly] = [target].[Monthly] AND [source].[Weekly] = [target].[Weekly]
	WHEN MATCHED THEN UPDATE SET 			
		 [target].[BatchJobId] =[source].[BatchJobId]
		,[target].[IsTotalLine]=[source].[IsTotalLine]
		,[target].[Metadata]=[source].[Metadata]		
		,[target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([Monthly],[Weekly],[IsTotalLine],[BatchJobId],[Metadata],[Version])  
		VALUES([source].[Monthly],[source].[Weekly],[source].[IsTotalLine],[source].[BatchJobId],[source].[Metadata],1);
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH