USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_DEL__DEFAU__57DD0BE4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] DROP CONSTRAINT [DF__TPROC_DEL__DEFAU__57DD0BE4]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_DEL__CREAT__58D1301D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] DROP CONSTRAINT [DF__TPROC_DEL__CREAT__58D1301D]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_DEL__ROW_S__59C55456]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] DROP CONSTRAINT [DF__TPROC_DEL__ROW_S__59C55456]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_DELIVERY_ADDRESS]    Script Date: 01/03/2018 12:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_DELIVERY_ADDRESS]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_DELIVERY_ADDRESS]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_DELIVERY_ADDRESS]    Script Date: 01/03/2018 12:34:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_DELIVERY_ADDRESS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DELIVERY_NAME] [varchar](50) NOT NULL,
	[DELIVERY_ADDRESS] [varchar](100) NOT NULL,
	[DEFAULT_IND] [varchar](1) NULL,
	[DELIVERY_PHONE] [varchar](20) NULL,
	[DELIVERY_FAX] [varchar](20) NULL,
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

ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] ADD  DEFAULT ('N') FOR [DEFAULT_IND]
GO

ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_DELIVERY_ADDRESS] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


