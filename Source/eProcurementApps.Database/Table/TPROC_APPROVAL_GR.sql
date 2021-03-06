USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DIVISION]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_APPROVAL_GR]'))
ALTER TABLE [dbo].[TPROC_APPROVAL_GR] DROP CONSTRAINT [FK_DIVISION]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__CREAT__753864A1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPROVAL_GR] DROP CONSTRAINT [DF__TPROC_APP__CREAT__753864A1]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__ROW_S__762C88DA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPROVAL_GR] DROP CONSTRAINT [DF__TPROC_APP__ROW_S__762C88DA]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPROVAL_GR]    Script Date: 01/03/2018 12:31:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_APPROVAL_GR]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_APPROVAL_GR]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPROVAL_GR]    Script Date: 01/03/2018 12:31:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_APPROVAL_GR](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[DEPARTMENT_NAME] [varchar](50) NULL,
	[DIVISION_ID] [int] NULL,
	[REV_WA_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_GR]  WITH CHECK ADD  CONSTRAINT [FK_DIVISION] FOREIGN KEY([DIVISION_ID])
REFERENCES [dbo].[TPROC_DIVISION] ([ID])
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_GR] CHECK CONSTRAINT [FK_DIVISION]
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_GR] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_APPROVAL_GR] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


