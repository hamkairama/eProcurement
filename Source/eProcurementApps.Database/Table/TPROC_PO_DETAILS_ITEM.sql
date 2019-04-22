USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PO_DETAILS]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_PO_DETAILS_ITEM]'))
ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] DROP CONSTRAINT [FK_PO_DETAILS]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PO___CREAT__5B438874]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] DROP CONSTRAINT [DF__TPROC_PO___CREAT__5B438874]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_PO___ROW_S__5C37ACAD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] DROP CONSTRAINT [DF__TPROC_PO___ROW_S__5C37ACAD]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_PO_DETAILS_ITEM]    Script Date: 01/03/2018 12:41:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_PO_DETAILS_ITEM]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_PO_DETAILS_ITEM]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_PO_DETAILS_ITEM]    Script Date: 01/03/2018 12:41:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_PO_DETAILS_ITEM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PO_HEADER_ID] [int] NOT NULL,
	[ITEM_NAME] [varchar](100) NULL,
	[PR_NO] [varchar](50) NULL,
	[UNITMEASUREMENT] [varchar](50) NULL,
	[QUANTITY] [int] NULL,
	[PRICE] [int] NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
	[ITEM_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM]  WITH CHECK ADD  CONSTRAINT [FK_PO_DETAILS] FOREIGN KEY([PO_HEADER_ID])
REFERENCES [dbo].[TPROC_PO_HEADERS] ([ID])
GO

ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] CHECK CONSTRAINT [FK_PO_DETAILS]
GO

ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_PO_DETAILS_ITEM] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO

