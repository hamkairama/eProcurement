USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WA_ALLOWED_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_WA_ALLOWED_DT]'))
ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] DROP CONSTRAINT [FK_WA_ALLOWED_GROUP]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WORK_AREA]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_WA_ALLOWED_DT]'))
ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] DROP CONSTRAINT [FK_WORK_AREA]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_WA___CREAT__10766AC2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] DROP CONSTRAINT [DF__TPROC_WA___CREAT__10766AC2]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_WA___ROW_S__116A8EFB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] DROP CONSTRAINT [DF__TPROC_WA___ROW_S__116A8EFB]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_WA_ALLOWED_DT]    Script Date: 01/03/2018 12:48:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_WA_ALLOWED_DT]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_WA_ALLOWED_DT]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_WA_ALLOWED_DT]    Script Date: 01/03/2018 12:48:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_WA_ALLOWED_DT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WA_ALLOWED_GROUP_ID] [int] NOT NULL,
	[WORK_AREA_ID] [int] NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT]  WITH CHECK ADD  CONSTRAINT [FK_WA_ALLOWED_GROUP] FOREIGN KEY([WA_ALLOWED_GROUP_ID])
REFERENCES [dbo].[TPROC_WA_ALLOWED_GR] ([ID])
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] CHECK CONSTRAINT [FK_WA_ALLOWED_GROUP]
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT]  WITH CHECK ADD  CONSTRAINT [FK_WORK_AREA] FOREIGN KEY([WORK_AREA_ID])
REFERENCES [dbo].[TPROC_WA] ([ID])
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] CHECK CONSTRAINT [FK_WORK_AREA]
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_WA_ALLOWED_DT] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


