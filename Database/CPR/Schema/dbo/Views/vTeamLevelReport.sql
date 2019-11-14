CREATE VIEW [dbo].[vTeamLevelReport]
	AS 
SELECT [report].*,[PhoneVolume].[Offered] AS pOffered,[PhoneVolume].[Handled] AS pHandled  FROM [dbo].[vExpandTeamLevelReportfromJsonMetadata] [report]
LEFT JOIN 
(
	SELECT [CreatedMonthly],[CreatedWeekNo],SUM([Offered]) AS  [Offered],SUM([Handled]) AS  [Handled]FROM [dbo].[TeamLevelPhoneVolume]
	GROUP BY [CreatedMonthly],[CreatedWeekNo]
) AS [PhoneVolume]
ON [report].[Monthly]=[PhoneVolume].[CreatedMonthly] AND [report].[Weekly]=[PhoneVolume].[CreatedWeekNo]
GO
