CREATE VIEW [dbo].[vMergedMSX]
	AS 
SELECT 'TQL' AS [MSXSourceName],[CreatedDaily],[CreatedMonthly],[CreatedWeekNo],[Agent],
[Lob],[SourceCampaign],[EstValue],NULL AS EstRevenue,[IsTQL],
0 AS [IsDirectWon],[CSSDispostion],[Status] FROM [dbo].[vMSXTql] 
WHERE Lob IS NOT NULL
UNION
SELECT 'SQO' AS [MSXSourceName],[CreatedDaily],[CreatedMonthly],[CreatedWeekNo],[Agent],
[Lob],[SourceCampaign],NULL AS [EstValue],EstRevenue,0 AS [IsTQL],
(CASE WHEN [Status]='Won' THEN 1 ELSE 0 END) AS [IsDirectWon],NULL AS [CSSDispostion], [Status] FROM [dbo].[vMSXSqo] 
WHERE Lob IS NOT NULL
