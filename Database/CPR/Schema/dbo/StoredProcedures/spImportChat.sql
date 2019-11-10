CREATE PROCEDURE [dbo].[spImportChat]
	@data [dbo].[ChatStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION	
	MERGE INTO [dbo].[Chat] AS [target]
	USING(
		SELECT 	[SkillSet],[Agent],[CreatedDateTime],[BatchJobId] FROM @data
	) AS [source]
	ON [source].[SkillSet] = [target].[SkillSet] AND [source].[Agent]=[target].[Agent] AND [source].[CreatedDateTime]=[target].[CreatedDateTime]
	WHEN MATCHED THEN UPDATE SET 					
		[target].[BatchJobId]= [source].[BatchJobId],			
		[target].[Version]= [target].[Version] + 1
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([SkillSet],[Agent],[CreatedDateTime],[BatchJobId],[Version])  
		VALUES([source].[SkillSet],[source].[Agent],[source].[CreatedDateTime],[source].[BatchJobId],1);
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
