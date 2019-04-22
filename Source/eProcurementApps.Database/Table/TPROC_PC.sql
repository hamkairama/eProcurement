USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__CREATE__1C873BEC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__CREATE__1C873BEC]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__ROW_ST__1D7B6025]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__ROW_ST__1D7B6025]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__IS_DIS__1E6F845E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__IS_DIS__1E6F845E]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__IS_VAT__1F63A897]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__IS_VAT__1F63A897]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__IS_PPH__2057CCD0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__IS_PPH__2057CCD0]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__IS_ACK__214BF109]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__IS_ACK__214BF109]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PC__FOR_ST__22401542]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PC] DROP CONSTRAINT [DF__TPROC_PC__FOR_ST__22401542]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_PC]    Script Date: 01/03/2018 12:40:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_PC]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_PC]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_PC]    Script Date: 01/03/2018 12:40:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_PC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PC_NUM] [varchar](20) NULL,
	[PO_TYPE_NM] [varchar](20) NOT NULL,
	[RECOM_SUPPLIER_NM] [varchar](100) NULL,
	[RECOM_SUPPLIER_ID] [int] NOT NULL,
	[RECOM_SUPPLIER_CP] [varchar](50) NOT NULL,
	[RECOM_SUPPLIER_PHONE] [varchar](100) NOT NULL,
	[RECOM_SUPPLIER_FAX] [varchar](50) NULL,
	[RECOM_SUPPLIER_ADDRESS] [varchar](200) NOT NULL,
	[DELIVERY_NM] [varchar](50) NOT NULL,
	[DELIVERY_ID] [int] NOT NULL,
	[DELIVERY_DATE] [datetime] NOT NULL,
	[DELIVERY_PHONE] [varchar](20) NOT NULL,
	[DELIVERY_FAX] [varchar](20) NULL,
	[DELIVERY_ADDRESS] [varchar](200) NOT NULL,
	[GRAND_TOTAL] [int] NOT NULL,
	[NOTE_BY_USER] [varchar](200) NULL,
	[NOTE_BY_EPROC] [varchar](200) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[IS_DISC_PERC] [int] NULL,
	[IS_VAT_PERC] [int] NULL,
	[IS_PPH_PERC] [int] NULL,
	[STATUS] [varchar](200) NULL,
	[NOTES] [varchar](200) NULL,
	[CURRENCY] [varchar](200) NULL,
	[IS_ACKNOWLEDGE_USER] [int] NULL,
	[FOR_STORAGE] [int] NULL,
	[FILE_NAME] [varchar](200) NULL,
	[ADD_COST] [int] NULL,
	[FINAL_TOTAL] [int] NULL,
	[ADD_COST_DESC] [varchar](200) NULL,
	[REJECT_REASON] [varchar](200) NULL,
	[REJECT_BY] [varchar](8) NULL,
	[REJECT_DATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNIQUE_TPROC_PC] UNIQUE NONCLUSTERED 
(
	[PC_NUM] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [IS_DISC_PERC]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [IS_VAT_PERC]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [IS_PPH_PERC]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [IS_ACKNOWLEDGE_USER]
GO

ALTER TABLE [dbo].[TPROC_PC] ADD  DEFAULT ((0)) FOR [FOR_STORAGE]
GO


