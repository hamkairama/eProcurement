USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_APPROVAL_GROUP_WA]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_WA]'))
ALTER TABLE [dbo].[TPROC_WA] DROP CONSTRAINT [FK_APPROVAL_GROUP_WA]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_WA__CREATE__1387E197]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_WA] DROP CONSTRAINT [DF__TPROC_WA__CREATE__1387E197]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_WA__ROW_ST__147C05D0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_WA] DROP CONSTRAINT [DF__TPROC_WA__ROW_ST__147C05D0]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_WA]    Script Date: 01/03/2018 12:48:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_WA]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_WA]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_WA]    Script Date: 01/03/2018 12:48:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_WA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WA_NUMBER] [int] NOT NULL,
	[APPROVAL_GROUP_ID] [int] NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNIQUE_WORK_AREA] UNIQUE NONCLUSTERED 
(
	[WA_NUMBER] ASC,
	[ROW_STATUS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_WA]  WITH CHECK ADD  CONSTRAINT [FK_APPROVAL_GROUP_WA] FOREIGN KEY([APPROVAL_GROUP_ID])
REFERENCES [dbo].[TPROC_APPROVAL_GR] ([ID])
GO

ALTER TABLE [dbo].[TPROC_WA] CHECK CONSTRAINT [FK_APPROVAL_GROUP_WA]
GO

ALTER TABLE [dbo].[TPROC_WA] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_WA] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


