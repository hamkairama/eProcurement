USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__CREAT__70A8B9AE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_TYPE] DROP CONSTRAINT [DF__TPROC_FOR__CREAT__70A8B9AE]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__ROW_S__719CDDE7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_TYPE] DROP CONSTRAINT [DF__TPROC_FOR__ROW_S__719CDDE7]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__IS_GO__72910220]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_TYPE] DROP CONSTRAINT [DF__TPROC_FOR__IS_GO__72910220]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_TYPE]    Script Date: 01/03/2018 12:37:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_TYPE]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_FORM_TYPE]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_TYPE]    Script Date: 01/03/2018 12:37:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_FORM_TYPE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FORM_TYPE_NAME] [varchar](50) NOT NULL,
	[FORM_TYPE_DESCRIPTION] [varchar](100) NULL,
	[CREATED_TIME] [date] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[IS_GOOD_TYPE] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNIQUE_FORM_TYPE] UNIQUE NONCLUSTERED 
(
	[FORM_TYPE_NAME] ASC,
	[ROW_STATUS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_FORM_TYPE] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_FORM_TYPE] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

ALTER TABLE [dbo].[TPROC_FORM_TYPE] ADD  DEFAULT ((0)) FOR [IS_GOOD_TYPE]
GO


