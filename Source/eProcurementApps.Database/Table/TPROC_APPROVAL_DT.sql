USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_APPROVAL_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_APPROVAL_DT]'))
ALTER TABLE [dbo].[TPROC_APPROVAL_DT] DROP CONSTRAINT [FK_APPROVAL_GROUP]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LEVEL_APPROVAL_DT]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_APPROVAL_DT]'))
ALTER TABLE [dbo].[TPROC_APPROVAL_DT] DROP CONSTRAINT [FK_LEVEL_APPROVAL_DT]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__CREAT__6E8B6712]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPROVAL_DT] DROP CONSTRAINT [DF__TPROC_APP__CREAT__6E8B6712]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__ROW_S__6F7F8B4B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPROVAL_DT] DROP CONSTRAINT [DF__TPROC_APP__ROW_S__6F7F8B4B]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPROVAL_DT]    Script Date: 01/03/2018 12:31:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_APPROVAL_DT]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_APPROVAL_DT]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPROVAL_DT]    Script Date: 01/03/2018 12:31:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_APPROVAL_DT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[APPROVAL_GROUP_ID] [int] NOT NULL,
	[APPROVAL_NAME] [varchar](50) NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[FLOW_NUMBER] [int] NOT NULL,
	[LEVEL_ID] [int] NULL,
	[EMAIL] [varchar](100) NULL,
	[USER_NAME] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT]  WITH CHECK ADD  CONSTRAINT [FK_APPROVAL_GROUP] FOREIGN KEY([APPROVAL_GROUP_ID])
REFERENCES [dbo].[TPROC_APPROVAL_GR] ([ID])
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT] CHECK CONSTRAINT [FK_APPROVAL_GROUP]
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT]  WITH CHECK ADD  CONSTRAINT [FK_LEVEL_APPROVAL_DT] FOREIGN KEY([LEVEL_ID])
REFERENCES [dbo].[TPROC_LEVEL] ([ID])
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT] CHECK CONSTRAINT [FK_LEVEL_APPROVAL_DT]
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_DT] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

