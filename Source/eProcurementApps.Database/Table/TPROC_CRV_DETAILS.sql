USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_CRV__CREAT__4F12BBB9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_CRV_DETAILS] DROP CONSTRAINT [DF__TPROC_CRV__CREAT__4F12BBB9]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_CRV__ROW_S__5006DFF2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_CRV_DETAILS] DROP CONSTRAINT [DF__TPROC_CRV__ROW_S__5006DFF2]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_CRV_DETAILS]    Script Date: 01/03/2018 12:34:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_CRV_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_CRV_DETAILS]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_CRV_DETAILS]    Script Date: 01/03/2018 12:34:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_CRV_DETAILS](
	[ID] [int] NOT NULL,
	[CRV_HEADER_ID] [int] NOT NULL,
	[ITEM_ID] [int] NULL,
	[ITEM_NAME] [varchar](100) NULL,
	[UNITMEASUREMENT] [varchar](50) NULL,
	[QUANTITY] [int] NULL,
	[PRICE] [int] NULL,
	[SUBTOTAL] [int] NULL,
	[FUND_T1] [varchar](10) NULL,
	[LOB1_T2] [varchar](10) NULL,
	[PLAN_T3] [varchar](10) NULL,
	[WA_T4] [varchar](10) NULL,
	[LOB2_T5] [varchar](10) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[CURRENCY] [int] NULL,
	[ACCOUNT_CODE] [varchar](8) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_CRV_DETAILS] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_CRV_DETAILS] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

