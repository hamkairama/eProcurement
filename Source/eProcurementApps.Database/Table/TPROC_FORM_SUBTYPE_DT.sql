USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TPROC_SUBTYPE_DT_FT]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_SUBTYPE_DT]'))
ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] DROP CONSTRAINT [FK_TPROC_SUBTYPE_DT_FT]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TPROC_SUBTYPE_DT_RD]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_SUBTYPE_DT]'))
ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] DROP CONSTRAINT [FK_TPROC_SUBTYPE_DT_RD]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__CREAT__3BCADD1B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] DROP CONSTRAINT [DF__TPROC_FOR__CREAT__3BCADD1B]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__ROW_S__3CBF0154]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] DROP CONSTRAINT [DF__TPROC_FOR__ROW_S__3CBF0154]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__FLOW___3DB3258D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] DROP CONSTRAINT [DF__TPROC_FOR__FLOW___3DB3258D]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_SUBTYPE_DT]    Script Date: 01/03/2018 12:36:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_SUBTYPE_DT]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_FORM_SUBTYPE_DT]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_SUBTYPE_DT]    Script Date: 01/03/2018 12:36:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_FORM_SUBTYPE_DT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[REL_DEPT_ID] [int] NOT NULL,
	[FORM_SUBTYPE_GR_ID] [int] NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[FLOW_NUMBER] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT]  WITH CHECK ADD  CONSTRAINT [FK_TPROC_SUBTYPE_DT_FT] FOREIGN KEY([FORM_SUBTYPE_GR_ID])
REFERENCES [dbo].[TPROC_FORM_SUBTYPE_GR] ([ID])
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] CHECK CONSTRAINT [FK_TPROC_SUBTYPE_DT_FT]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT]  WITH CHECK ADD  CONSTRAINT [FK_TPROC_SUBTYPE_DT_RD] FOREIGN KEY([REL_DEPT_ID])
REFERENCES [dbo].[TPROC_REL_DEPT] ([ID])
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] CHECK CONSTRAINT [FK_TPROC_SUBTYPE_DT_RD]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUBTYPE_DT] ADD  DEFAULT ((0)) FOR [FLOW_NUMBER]
GO


