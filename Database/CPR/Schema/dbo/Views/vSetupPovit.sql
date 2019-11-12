CREATE VIEW [dbo].[vSetupPovit]
	AS 
SELECT 
[talks].[CreatedDaily],
[talks].[CreatedMonthly],
[talks].[CreatedWeekNo],
[talks].[Lob],[talks].[Agent],
[talks].[Volume],
[msx].[IsTQL],
[msx].[IsDirectWon],
[msx].[Status],
[msx].[CSSDispostion],
[msx].[EstRevenue],
[msx].[EstValue]
FROM [dbo].[vTotalTalks] [talks]
LEFT JOIN [dbo].[vMergedMSX] [msx]
	ON [talks].[Agent] = [msx].[Agent] AND [talks].[CreatedDaily]=[msx].CreatedDaily 