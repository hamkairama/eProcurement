USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_SUP__CREAT__40C49C62]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_SUPPLIER] DROP CONSTRAINT [DF__TPROC_SUP__CREAT__40C49C62]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_SUP__ROW_S__41B8C09B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_SUPPLIER] DROP CONSTRAINT [DF__TPROC_SUP__ROW_S__41B8C09B]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_SUPPLIER]    Script Date: 01/03/2018 12:47:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_SUPPLIER]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_SUPPLIER]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_SUPPLIER]    Script Date: 01/03/2018 12:47:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_SUPPLIER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SUPPLIER_NAME] [varchar](100) NOT NULL,
	[SUPPLIER_ALIAS_NAME] [varchar](50) NULL,
	[SUPPLIER_ADDRESS] [varchar](200) NULL,
	[VENDOR_CODE] [varchar](50) NOT NULL,
	[BANK_NAME] [varchar](50) NULL,
	[BANK_BRANCH] [varchar](50) NULL,
	[BANK_ACCOUNT_NUMBER] [varchar](50) NULL,
	[CONTACT_PERSON] [varchar](50) NOT NULL,
	[EMAIL_ADDRESS] [varchar](50) NULL,
	[MOBILE_NUMBER] [varchar](100) NULL,
	[OFFICE_NUMBER] [varchar](50) NULL,
	[FAX_NUMBER] [varchar](50) NULL,
	[TAX_NUMBER] [varchar](50) NULL,
	[WEBSITE] [varchar](50) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[NPWP] [varchar](50) NULL,
	[DESCRIPTION] [varchar](200) NULL,
	[CORE_BUSINESS] [varchar](200) NULL,
	[NAMA_BARANG] [varchar](100) NULL,
	[B_UNIT_OWNER] [varchar](50) NULL,
	[CITY] [varchar](100) NULL,
	[EFFECTIVE_DATE] [varchar](20) NULL,
	[SCHEDULE_EVALUATION] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNIQUE_SUPPLIER] UNIQUE NONCLUSTERED 
(
	[VENDOR_CODE] ASC,
	[ROW_STATUS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_SUPPLIER] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_SUPPLIER] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


