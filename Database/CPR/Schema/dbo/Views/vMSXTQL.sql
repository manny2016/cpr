CREATE VIEW [dbo].[vMSXTql]
	AS 
SELECT [LeadId],[mapLob].[To] AS Lob,[Agent],[Country],[Status],[EstValue],[Currency],
(CASE  WHEN  [mapTQL].[To] IS NULL THEN 0 ELSE 1 END) AS IsTQL, 
[CSSDispostion],[SourceCampaign],[CreatedMonthly],[CreatedDaily],[CreatedDateTime],[CreatedWeekNo]
FROM [dbo].[msxWithTQL] (NOLOCK) [tql]
	LEFT JOIN  [dbo].[Mappings] (NOLOCK) [mapTQL] 
		ON [tql].[CSSDispostion] = [mapTQL].[From] AND [mapTQL].[Name] ='cssDispostion-TQL'
	LEFT JOIN  [dbo].[Mappings] (NOLOCK) [mapLob] 
		ON [tql].[SourceCampaign] = [mapLob].[From] AND [mapLob].[Name]='campain->Lob'
