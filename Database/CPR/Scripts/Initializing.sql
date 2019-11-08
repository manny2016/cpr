﻿MERGE INTO [dbo].[Mappings] [target]
USING(
	VALUES
	('skillSet->country',	'Office 365 Chat-CN',				'China'),
	('skillSet->country',	'Office 365 Chat-HK',				'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_CPR_MC_R',						'China'),		
	('skillSet->country',	'WX_CPR_CC_R',						'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_CPR_CC_S',						'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_CPR_MC_Sales',					'China'),
	('skillSet->country',	'WX_CPR_CC_Sales',					'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'Dynamics chat',					'China'),	
	('skillSet->country',	'WX_CPR_Dyn_Sales',					'China'),
	('skillSet->country',	'WX_CPR_PRC_CustomerM',				'China'),
	('skillSet->country',	'WX_HK_SM_Mcorp',					'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_HK_SM_Corp',					'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_HK_SM_ENCorp',					'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_HK_Piracy_Mcorp',				'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_HK_Piracy_ENCorp',				'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_HK_Piracy_Corp',				'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'AZURE-LEADGEN-ZH-CN',				'China'),
	('skillSet->country',	'WX_CPR_CC_Azure_Sale',				'Hong Kong SAR and Macao SAR'),
	('skillSet->country',	'WX_CPR_Azure_Sales',				'China'),
	('skillSet->country',	'WX_CPR_EN_S',						'Australia, India, Malaysia, Singapore, New Zealand, Bangladesh, Brunei, Fiji, Indonesia, Sri Lanka, Vietnam, Cambodia, Philippines'),
	('skillSet->country',	'WX_CPR_EN_R',						'Australia, India, Malaysia, Singapore, New Zealand, Bangladesh, Brunei, Fiji, Indonesia, Sri Lanka, Vietnam, Cambodia, Philippines'),

	('skillSet->Lob',		'Office 365 Chat-CN',				'Modern Workplace'),
	('skillSet->Lob',		'Office 365 Chat-HK',				'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_MC_R',						'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_MC_S',						'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_CC_R',						'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_CC_S',						'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_MC_Sales',					'Modern Workplace'),
	('skillSet->Lob',		'WX_CPR_CC_Sales ',					'Modern Workplace'),
	('skillSet->Lob',		'Dynamics chat',					'Dynamics'),
	('skillSet->Lob',		'WX_CPR_Dyn_Sales',					'Dynamics'),
	('skillSet->Lob',		'WX_CPR_PRC_CustomerM',				'Main IVR'),
	('skillSet->Lob',		'WX_HK_SM_Mcorp',					'Main IVR'),	
	('skillSet->Lob',		'WX_HK_SM_Corp',					'Main IVR'),	
	('skillSet->Lob',		'WX_HK_SM_ENCorp',					'Main IVR'),	
	('skillSet->Lob',		'WX_HK_Piracy_Mcorp',				'Main IVR'	),
	('skillSet->Lob',		'WX_HK_Piracy_ENCorp',				'Main IVR'	),
	('skillSet->Lob',		'WX_HK_Piracy_Corp',				'Main IVR'	),
	('skillSet->Lob',		'AZURE-LEADGEN-ZH-CN',				'Azure'),
	('skillSet->Lob',		'WX_CPR_CC_Azure_Sale',				'Azure'),
	('skillSet->Lob',		'WX_CPR_Azure_Sales',				'Azure'),
	('skillSet->Lob',		'WX_CPR_EN_S',						'Cloud IVR(EN)'	),
	('skillSet->Lob',		'WX_CPR_EN_R',						'Cloud IVR(EN)'),
	('skillSet->Lob',		'PRODUCTIVITY-LEADGEN-ZH-CN',		'Modern Workplace'),		
	('skillSet->Lob',		'PRODUCTIVITY-LEADGEN-ZH-HK',		'Modern Workplace'),	
	('skillSet->Lob',		'DYNAMICS-LEADGEN-ZH-CN',			'Dynamics'),
	
	('campain->Lob','ASIA - Wicresoft - China, Hong Kong - Office 365 Commercial - O365 - Web Phone',	'Modern Workplace'),
	('campain->Lob','ASIA - Wicresoft - China - Dynamics 365 - Dynamics - Button Chat',					'Dynamics'),
	('campain->Lob','ASIA - Wicresoft - China - Dynamics 365 - Dynamics - Proactive Chat',				'Dynamics'),
	('campain->Lob','ASIA - Wicresoft - China - Dynamics 365 - Dynamics - Web Phone',					'Dynamics'),
	('campain->Lob','ASIA - Wicresoft - China, Hong Kong - All Products - On-Prem (Main IVR) - Standard Phone','Main IVR'),
	('campain->Lob','ASIA - Wicresoft - China - Azure - Azure - Button Chat',							'Azure'),
	('campain->Lob','ASIA - Wicresoft - China, Hong Kong - Azure - Azure - Web Phone',					'Azure'),
	('campain->Lob','ASIA - Wicresoft - GCR, India, APAC - All Cloud Products - Cloud IVR - Standard Phone','Cloud IVR(EN)'),
	
	('chatAgent->Agent','wic_wux_allenli','Allen Li'),
	('chatAgent->Agent','wic_wux_autumnni','Qiumi Ni'),
	('chatAgent->Agent','wic_wux_calliewu','Callie Wu'),
	('chatAgent->Agent','wic_wux_carolinemo','Caroline Mo'),
	('chatAgent->Agent','wic_wux_crystalgao','Dandan Gao'),
	('chatAgent->Agent','wic_wux_darcywang','Darcy Wang'),
	('chatAgent->Agent','wic_wux_gloriawang','Gloria Wang'),
	('chatAgent->Agent','wic_wux_jellyzhang','Jelly Zhang'),
	('chatAgent->Agent','wic_wux_katynawang','Katyna Wang'),
	('chatAgent->Agent','wic_wux_kayleezhou','Kaylee Zhou'),
	('chatAgent->Agent','wic_wux_lindajiang','Linda Jiang'),
	('chatAgent->Agent','wic_wux_maywu','May Wu'),
	('chatAgent->Agent','wic_wux_namarazhang','Namara Zhang'),
	('chatAgent->Agent','wic_wux_oliviaxia','Olivia Xia'),
	('chatAgent->Agent','wic_wux_rubylong','Ruby Long'),
	('chatAgent->Agent','wic_wux_wilsontan','Wilson Tan'),
	('chatAgent->Agent','wic_wux_winnieluo','Winnie Luo'),
	('chatAgent->Agent','wix_wux_vickyjiang','Vicky Jiang')

)[source]([Name],[From],[To])
ON [target].[Name]=[source].[Name] AND [target].[From] = [source].[From]
WHEN MATCHED THEN UPDATE SET [target].[To]=[source].[To]
WHEN NOT MATCHED THEN
INSERT([Name], [From], [To])
VALUES([source].[Name], [source].[From], [source].[To]);