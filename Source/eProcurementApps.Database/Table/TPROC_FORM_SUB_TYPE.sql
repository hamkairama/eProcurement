USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TPROC_SUBTYPE_GR_FST]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_SUB_TYPE]'))
ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] DROP CONSTRAINT [FK_TPROC_SUBTYPE_GR_FST]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__CREAT__2D7CBDC4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] DROP CONSTRAINT [DF__TPROC_FOR__CREAT__2D7CBDC4]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_FOR__ROW_S__2E70E1FD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] DROP CONSTRAINT [DF__TPROC_FOR__ROW_S__2E70E1FD]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_SUB_TYPE]    Script Date: 01/03/2018 12:35:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_FORM_SUB_TYPE]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_FORM_SUB_TYPE]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_FORM_SUB_TYPE]    Script Date: 01/03/2018 12:35:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_FORM_SUB_TYPE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[SUB_FORMTYPE_GR_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE]  WITH CHECK ADD  CONSTRAINT [FK_TPROC_SUBTYPE_GR_FST] FOREIGN KEY([SUB_FORMTYPE_GR_ID])
REFERENCES [dbo].[TPROC_FORM_SUBTYPE_GR] ([ID])
GO

ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] CHECK CONSTRAINT [FK_TPROC_SUBTYPE_GR_FST]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_FORM_SUB_TYPE] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


