CREATE PROCEDURE [dbo].[spImportAvaya]
	@data [dbo].[AvayaStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[Avaya] AS [target]
	USING(
		SELECT 	[SkillSet],[Agent],[CreatedDateTime],[BatchJobId],[Volume] FROM @data
	) AS [source]
	ON [source].[SkillSet] = [target].[SkillSet] AND [source].[Agent]=[target].[Agent] AND [source].[CreatedDateTime]=[target].[CreatedDateTime]
	WHEN MATCHED THEN UPDATE SET 					
		[target].[BatchJobId]= [source].[BatchJobId],	
		[target].[Volume]= [source].[Volume],			
		[target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([SkillSet],[BatchJobId],[Agent],[CreatedDateTime],[Volume],[Version])  
		VALUES([source].[SkillSet],[source].[BatchJobId],[source].[Agent],[source].[CreatedDateTime],[source].[Volume],1);
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
