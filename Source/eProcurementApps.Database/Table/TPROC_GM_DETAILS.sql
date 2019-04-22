USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_GM___CREAT__7A3223E8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_GM_DETAILS] DROP CONSTRAINT [DF__TPROC_GM___CREAT__7A3223E8]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_GM___ROW_S__7B264821]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_GM_DETAILS] DROP CONSTRAINT [DF__TPROC_GM___ROW_S__7B264821]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_GM_DETAILS]    Script Date: 01/03/2018 12:37:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_GM_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_GM_DETAILS]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_GM_DETAILS]    Script Date: 01/03/2018 12:37:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_GM_DETAILS](
	[ID] [int] NOT NULL,
	[ITEM_ID] [int] NULL,
	[ITEM_NAME] [varchar](100) NULL,
	[UNITMEASUREMENT] [varchar](50) NULL,
	[QUANTITY] [int] NULL,
	[PRICE] [int] NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[GM_ID] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_GM_DETAILS] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_GM_DETAILS] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


