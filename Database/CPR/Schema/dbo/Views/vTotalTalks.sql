CREATE VIEW [dbo].[vTotalTalks]
AS
SELECT 
	[avaya].[CreatedMonthly],
	[lobs].[To] AS [Lob],
	[SkillSet],
	[Agent],
	[region].[To] AS Region,
	'Modern Workplace-Web Phone' AS [Channel],
	[Volume],
	[CreatedDaily],
	[CreatedWeekNo]	
	FROM [dbo].[Avaya] [avaya]
	LEFT JOIN [dbo].[Mappings] (NOLOCK) [lobs]
		ON [avaya].[SkillSet] = [lobs].[From] AND [lobs].[Name]='skillSet->Lob'
	LEFT JOIN [dbo].[Mappings] (NOLOCK) [region] 
		ON [avaya].[SkillSet] = [region].[From] AND [region].[Name]='skillSet->country'
WHERE [lobs].[To] IS NOT NULL
UNION 
SELECT 
	[chat].[CreatedMonthly],
	[lobs].[To] AS [Lob],
	[SkillSet],
	[Agent],
	[region].[To] AS Region, 
	'Modern Workplace-Chat' AS [Channel], 
	1 AS [Volume],
	[CreatedDaily],
	[CreatedWeekNo] 
FROM [dbo].[Chat] (NOLOCK) [chat]	
LEFT JOIN [dbo].[Mappings] (NOLOCK) [lobs]
	ON [chat].[SkillSet] = [lobs].[From] AND [lobs].[Name]='skillSet->Lob'
LEFT JOIN [dbo].[Mappings] (NOLOCK) [region] 
	ON [chat].[SkillSet] = [region].[From] AND [region].[Name]='skillSet->country'
WHERE [lobs].[To] IS NOT NULL

