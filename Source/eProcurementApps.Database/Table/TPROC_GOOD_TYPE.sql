USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_GOO__CREAT__7FEAFD3E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_GOOD_TYPE] DROP CONSTRAINT [DF__TPROC_GOO__CREAT__7FEAFD3E]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_GOO__ROW_S__00DF2177]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_GOOD_TYPE] DROP CONSTRAINT [DF__TPROC_GOO__ROW_S__00DF2177]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_GOOD_TYPE]    Script Date: 01/03/2018 12:38:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_GOOD_TYPE]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_GOOD_TYPE]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_GOOD_TYPE]    Script Date: 01/03/2018 12:38:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_GOOD_TYPE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GOOD_TYPE_NAME] [varchar](50) NOT NULL,
	[GOOD_TYPE_DESCRIPTION] [varchar](100) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNIQUE_GOOD_TYPE] UNIQUE NONCLUSTERED 
(
	[GOOD_TYPE_NAME] ASC,
	[ROW_STATUS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_GOOD_TYPE] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_GOOD_TYPE] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

