CREATE VIEW [dbo].[vExpandTeamLevelReportfromJsonMetadata]
	AS 

SELECT 
[Monthly],
[Weekly],
(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of Impression"')
) AS[Sum of Impression],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Click Through"')
) AS[Click Through],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of Offered"')
) AS[Sum of Offered],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Conv(HandledToOffered)"')
) AS[Conv(HandledToOffered)],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of Handled"')
) AS[Sum of Handled],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of TotalLeads_TQL"')
) AS[Sum of TotalLeads_TQL],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of NonSalesProspect"')
) AS[Sum of NonSalesProspect],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Conv(LeadsToNon-SalesProspect)"')
) AS[Conv(LeadsToNon-SalesProspect)],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of SalesProspect"')
) AS[Sum of SalesProspect],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Conv(LeadsToSalesLeads)"')
) AS[Conv(LeadsToSalesLeads)],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of Kickbacks"')
) AS[Sum of Kickbacks],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of TotalTQL"')
) AS[Sum of TotalTQL],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of IsTQLAccepted"')
) AS[Sum of IsTQLAccepted],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of IsTQLRejected"')
) AS[Sum of IsTQLRejected],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of RecipientDisqualifiedTQL"')
) AS[Sum of RecipientDisqualifiedTQL],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of TotalTQLEstRevUSD"')
) AS[Sum of TotalTQLEstRevUSD],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."TotalTQLEstRevUSD/TQL"')
) AS[TotalTQLEstRevUSD/TQL],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Conv(SQOsorPCDealsToConvertedProspect)"')
) AS[Conv(SQOsorPCDealsToConvertedProspect)],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of SQOsOrPCDeals"')
) AS[Sum of SQOsOrPCDeals],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."SQOsOrPCDeal_Est. Revenue"')
) AS[SQOsOrPCDeal_Est. Revenue],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."WinRate(SQOorPCDeal)"')
) AS[WinRate(SQOorPCDeal)],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of WonSQOsOrPCDeals"')
) AS[Sum of WonSQOsOrPCDeals],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of WonClosedRevenue"')
) AS[Sum of WonClosedRevenue],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of InfluencedRevenue"')
) AS[Sum of InfluencedRevenue],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of CloseDateWonSQOsOrPCDeals"')
) AS[Sum of CloseDateWonSQOsOrPCDeals],(
    SELECT[JValue] FROM
    OPENJSON(Metadata)
    WITH([JValue] NVARCHAR(MAX) '$."Sum of CloseDateWonClosedRevenue"')
) AS[Sum of CloseDateWonClosedRevenue]
FROM [dbo].[TeamLevelReport]
WHERE IsTotalLine = 0;

