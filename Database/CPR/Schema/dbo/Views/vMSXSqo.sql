CREATE VIEW [dbo].[vMSXSqo]
	AS 
SELECT [OpportunityId],[Agent],[Country],[Status],[EstRevenue],[Currency],
[mapLob].[To] AS Lob,
[SourceCampaign],[sqo].[CreatedMonthly],[CreatedDaily],[CreatedDateTime],[CreatedWeekNo]
FROM [dbo].[msxWithSQO] (NOLOCK) [sqo]	
	LEFT JOIN  [dbo].[Mappings] (NOLOCK) [mapLob] 
		ON [sqo].[SourceCampaign] = [mapLob].[From] AND [mapLob].[Name]='campain->Lob'