CREATE PROCEDURE [dbo].[spImportTeamLevelPhoneVolume]
	@data [dbo].[TeamLevelPhoneVolumeStructure] READONLY
AS
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[TeamLevelPhoneVolume] AS [target]
	USING(
		SELECT 	[Channel],[Program],[Region],[Market],[Supplier],[Offered],[Handled],[BatchJobId],[CreatedDateTime] FROM @data
	) AS [source]
	ON [source].[CreatedDateTime] = [target].[CreatedDateTime] AND [source].[Channel] = [target].[Channel]
		AND [source].[Region] = [target].[Region] AND [source].[Supplier] = [target].[Supplier]		AND [source].[Market] = [target].[Market]
	WHEN MATCHED THEN UPDATE SET 			
		 [target].[Offered] = [source].[Offered],
		 [target].[Handled] = [source].[Handled],
		 [target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT(
[Channel],
[Program],
[Region],
[Market],
[Supplier],
[Offered],
[Handled],
[BatchJobId],
[CreatedDateTime],
[Version])  
		VALUES(
[source].[Channel],
[source].[Program],
[source].[Region],
[source].[Market],
[source].[Supplier],
[source].[Offered],
[source].[Handled],
[source].[BatchJobId],
[source].[CreatedDateTime],
1);
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH


